using System;
using System.Collections;
using System.Text;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Pins;
//using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Storage.Streams;
using GHIElectronics.TinyCLR.Devices.Uart;
using System.Runtime.CompilerServices;
//using System.Drawing;
using System.Diagnostics;
using GHIElectronics.TinyCLR.Devices.Rtc;

namespace TinyCLR_F769
{
    class Program
    {
        static void Main()
        {
            RtcController rtc = RtcController.GetDefault();
            try
            {
                var t = rtc.GetTime();
                if ( t.Year == 1980 )
                {
                    rtc.SetTime(RtcDateTime.FromDateTime(new DateTime(2018, 10, 12, 17, 51, 00)));
                }
            }
            catch (Exception)
            {
                rtc.SetTime(RtcDateTime.FromDateTime(new DateTime(2018, 10, 11, 11, 51, 00)));
            }

            //string COM = "GHIElectronics.TinyCLR.NativeApis.STM32F7.UartController\\2";
            UartController ser = UartController.FromName(STM32F7.UartPort.Usart1); // DISCO-F769
            //UartController ser = UartController.FromName(STM32F7.UartPort.Usart3); // NUCLEO-F767
            ser.SetActiveSettings(38400, 8, UartParity.None, UartStopBitCount.One, UartHandshake.None);
            Debug.WriteLine("Starting Program ....");
            BlinkLed blink = new BlinkLed(STM32F7.GpioPin.PJ5, 500); // Disco 769
            //BlinkLed blink = new BlinkLed(STM32F7.GpioPin.PB7, 500); // Nucleo 144 - F767
            Thread run = new Thread(blink.Blink);
            run.Start();
            var r = new HeapAllocTest(10);
            r.Allocate();
            //var d = new DisplayTest();
            string sallocated = "";
            //Debug.WriteLine("Allocated TOTAL Memory:" + GC.GetTotalMemory(false).ToString() + " bytes!");
            //d.DrawSomething("Test String...", 50,50);

            int x = 10, y = 10;

            //SDTest sd = new SDTest();

            sallocated = "Memory:" + GC.GetTotalMemory(true).ToString() + " bytes!";
            while(true)
            {
                x += 2;
                y += 2;
                if (x > (800 - 160) || y > 480 - 160)
                {
                    x = 10;
                    y = 10;
                    GC.Collect();
                }
                Thread.Sleep(1000);
                //d.DrawSomething(sallocated, x, y);
                var dt = rtc.Now;
                byte[] b = System.Text.Encoding.UTF8.GetBytes("Program Running !! .." + dt.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n");
                ser.Write(b);
                ser.Flush();

            }

        }
    }

}
