using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TinyCLR_F769
{
    public class HeapAllocTest
    {
        const int MAX_HEAP = 1024; // max Kbytes in the heap available

        public int MaxBytes { get; set; }

        public HeapAllocTest(int max = 0)
        {
            if ( max == 0)
            {
                MaxBytes = MAX_HEAP * 1024;
            }
            else
            {
                MaxBytes = max * 1024;
            }
        }

        public int Allocate()
        {
            int ret = 0;
            byte[] buffer = new byte[MaxBytes];
            Debug.WriteLine("Starting allocate memory ....");
            for (int i = 0; i < buffer.Length; i += 4)
            {
                buffer[i] = (byte)i;
                var zz = buffer[i];
                if (zz != (byte)i)
                {
                    Debug.WriteLine("OhOh ---> Write " + i.ToString() + ", get back " + zz.ToString());
                }

                if ( i % 1000  == 0)
                {
                    Debug.WriteLine("..Done " + i.ToString() + "Kb");
                }
            }            
            ret = (int) GC.GetTotalMemory(false);
            Debug.WriteLine("Allocated Heap Memory: " + ret.ToString() + " bytes...");

            return ret;
        }
    }
}
