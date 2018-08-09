using System;
using System.Collections;
using System.Text;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Pins;
using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Devices.Display.Provider;
using GHIElectronics.TinyCLR.Storage.Streams;
using GHIElectronics.TinyCLR.Devices.SerialCommunication;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Diagnostics;

namespace TinyCLR_F769
{
    public class DisplayTest
    {
        DisplayController _display = null;
        Graphics _screen = null;

        public DisplayTest()
        {
            _display = DisplayController.GetDefault();

            ParallelDisplayControllerSettings _settings = new ParallelDisplayControllerSettings()
            {
                Width = 800,
                Height = 480,
                PixelClockRate = 9600000, // not used in native code
                PixelPolarity = false,
                OutputEnablePolarity = true, // this must be true
                OutputEnableIsFixed = true,
                HorizontalFrontPorch = 8,
                HorizontalBackPorch = 43,
                HorizontalSyncPulseWidth = 2,
                HorizontalSyncPolarity = false,
                VerticalFrontPorch = 2,
                VerticalBackPorch = 2,
                VerticalSyncPulseWidth = 10,
                VerticalSyncPolarity = false,
                DataFormat = DisplayDataFormat.Rgb565 // not really correct: it is Rgb888 for Disco-746 display...

            };
                        
            _display.ApplySettings(_settings);
            _display.WriteString("\f\n\n* Discovery STM32F69 board *\n\n");
            _display.WriteString  ("* TinyCLR 0.12.0 for STM32F7 *");
            _screen = Graphics.FromHdc(_display.Hdc);
        }

        public void DrawSomething(string s, int x, int y)
        {
            Font f = Resource1.GetFont(Resource1.FontResources.NinaB);
            _screen.Clear(Color.Black);
            Pen pen = new Pen(Color.Green);
            _screen.FillRectangle(pen.Brush, x, y, 160, 160);
            _screen.DrawString(s, f, new SolidBrush(Color.Red), x, y + 170);
            _screen.Flush();
        }
    }
}
