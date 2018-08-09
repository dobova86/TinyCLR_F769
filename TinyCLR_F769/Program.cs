using System;
using System.Collections;
using System.Text;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Pins;
//using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Storage.Streams;
using GHIElectronics.TinyCLR.Devices.SerialCommunication;
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
            DataReader serReader;
            DataWriter serWriter;
            RtcController rtc = RtcController.GetDefault();
            rtc.Now = new DateTime(2018, 8, 9, 15,25,00);

            string COM = "GHIElectronics.TinyCLR.NativeApis.STM32F7.UartProvider\\0";
            SerialDevice ser = SerialDevice.FromId(UC5550.UartPort.Usart1); //SerialDevice.FromId(COM);
            ser.BaudRate = 38400;
            ser.ReadTimeout = TimeSpan.Zero;
            serReader = new DataReader(ser.InputStream);
            serWriter = new DataWriter(ser.OutputStream);
            Debug.WriteLine("Starting Program ....");
            BlinkLed blink = new BlinkLed(STM32F4.GpioPin.PJ5, 500);
            Thread run = new Thread(blink.Blink);
            run.Start();
            var r = new HeapAllocTest(10);
            r.Allocate();
            //var d = new DisplayTest();
            string sallocated = "";
            //Debug.WriteLine("Allocated TOTAL Memory:" + GC.GetTotalMemory(false).ToString() + " bytes!");
            serWriter.WriteString("Program Started !! .. \r\n");
            serWriter.Flush();
            //d.DrawSomething("Test String...", 50,50);
            int x = 10, y = 10;

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
                serWriter.WriteString("Program Running !! .." + rtc.Now.ToString() + "\r\n");
                serWriter.Store();

            }

        }
    }

}
