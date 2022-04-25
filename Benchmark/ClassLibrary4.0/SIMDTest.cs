using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary4_0
{
    public class SIMDTest
    {
        private static readonly byte[] XBytes = Enumerable.Range(0, 4096000).Select(i => (byte)i).ToArray();
        private static readonly byte[] YBytes = new byte[XBytes.Length];

        public SIMDTest()
        {
            XBytes[4095999] = 1;
            YBytes[4095999] = 2;
            Array.Copy(XBytes, 0, YBytes, 0, XBytes.Length - 1);
        }

        [BenchmarkDotNet.Attributes.Benchmark(Baseline = true)]
        public bool SimpleCompare()
        {
            for (int i = 0; i < XBytes.Length; i++)
            {
                if (XBytes[i] != YBytes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
