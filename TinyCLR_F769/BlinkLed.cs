using System;
using System.Collections;
using System.Text;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Pins;

namespace TinyCLR_F769
{
    public class BlinkLed
    {
        int PinNumber;
        GpioController _gpio = null;
        GpioPin _pin = null;
        //GpioPin _pin2 = null;
        //int pin2 = STM32F4.GpioPin.PB14;
        bool run = false;
        int Millisec = 0;

        public BlinkLed(int pin, int ms)
        {
            PinNumber = pin;
            Millisec = ms;
            _gpio = GpioController.GetDefault();
            _pin = _gpio.OpenPin(pin);
            //_pin2 = _gpio.OpenPin(pin2);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
            //_pin2.SetDriveMode(GpioPinDriveMode.Output);
            _pin.Write(GpioPinValue.Low);
            run = true;
        }

        public void Blink()
        {
            while(run)
            {
                _pin.Write(GpioPinValue.High);
                //_pin2.Write(GpioPinValue.Low);
                Thread.Sleep(Millisec);
                _pin.Write(GpioPinValue.Low);
                //_pin2.Write(GpioPinValue.High);
                Thread.Sleep(Millisec);
            }
        }
        
    }
}
