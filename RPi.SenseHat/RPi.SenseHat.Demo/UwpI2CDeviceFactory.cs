using RichardsTech.Sensors;
using RTIMULibCS;
using System;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Abstractions;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace Emmellsoft.IoT.RPi.SenseHat.Demo
{
    public class UwpI2CDeviceFactory : I2CDeviceFactory
    {
        public static void Init()
        {
            Init(new UwpI2CDeviceFactory());
        }

        public override async Task<II2CDevice> Create(byte deviceId)
        {
            string aqsFilter = I2cDevice.GetDeviceSelector("I2C1");

            DeviceInformationCollection collection = await DeviceInformation.FindAllAsync(aqsFilter);
            if (collection.Count == 0)
            {
                throw new SensorException("I2C device not found");
            }

            I2cConnectionSettings i2CSettings = new I2cConnectionSettings(deviceId)
            {
                BusSpeed = I2cBusSpeed.FastMode
            };

            I2cDevice device = await I2cDevice.FromIdAsync(collection[0].Id, i2CSettings);

            return new UwpI2CDevice(device);
        }

        private class UwpI2CDevice : II2CDevice
        {
            private readonly I2cDevice _i2CDevice;

            public UwpI2CDevice(I2cDevice i2CDevice)
            {
                _i2CDevice = i2CDevice;
            }

            public byte Read()
            {
                throw new System.NotImplementedException();
            }

            public byte[] Read(int length)
            {
                var buffer = new byte[length];
                _i2CDevice.Read(buffer);
                return buffer;
            }

            public void Write(byte data)
            {
                throw new System.NotImplementedException();
            }

            public void Write(byte[] data)
            {
                _i2CDevice.Write(data);
            }

            public void WriteAddressByte(int address, byte data)
            {
                throw new System.NotImplementedException();
            }

            public void WriteAddressWord(int address, ushort data)
            {
                throw new System.NotImplementedException();
            }

            public byte ReadAddressByte(int address)
            {
                throw new System.NotImplementedException();
            }

            public ushort ReadAddressWord(int address)
            {
                throw new System.NotImplementedException();
            }

            public int DeviceId { get; }

            public int FileDescriptor { get; }
        }
    }
}
