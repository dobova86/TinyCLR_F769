using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.IO;
using GHIElectronics.TinyCLR.Devices.Storage;
using GHIElectronics.TinyCLR.Devices.Storage.Provider;
using GHIElectronics.TinyCLR.Storage;
using GHIElectronics.TinyCLR.Storage.Streams;
using GHIElectronics.TinyCLR.IO;
using System.Diagnostics;

namespace TinyCLR_F769
{
    public class SDTest
    {
        private static StorageController sc = null;
        private static IStorageControllerProvider drive;

        public SDTest()
        {
            byte[] buffer = new byte[256];

            sc = StorageController.FromName(@"GHIElectronics.TinyCLR.NativeApis.STM32F7.SdCardStorageController\0");
            sc.Open();
            drive = sc.Provider;
            //try
            //{
            //    drive = FileSystem.Mount(sc.Hdc);
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //    Debug.WriteLine("ERROR SDCard:" + s);
            //}
            drive.Open();
            drive.Read(0, 256, buffer, 0, 10000);
            Debug.WriteLine("Read from card: " + buffer[0].ToString());
            //int n = drive.Write(100000L, 4, new byte[] { 0x55, 0xAA, 0x55, 0xAA }, 0, 100000);
            //Debug.WriteLine("Write to card: " + n.ToString());
            drive.Close();
            sc.Close();
        }
    }
}
