using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
#if !NET4_0
using System.Numerics;
#endif
#if NETCOREAPP2_2_OR_GREATER
using System.Runtime.Intrinsics.X86;
#endif

namespace B_ClassLibrary
{
    public class SIMDTest
    {
        private static readonly byte[] XBytes = Enumerable.Range(0, 4096010).Select(i => (byte)i).ToArray();
        private static readonly byte[] YBytes = new byte[XBytes.Length];

        public SIMDTest()
        {
            XBytes[XBytes.Length - 1] = 1;
            YBytes[XBytes.Length - 1] = 2;
            Array.Copy(XBytes, 0, YBytes, 0, XBytes.Length - 1);
#if DEBUG
            XBytes[400] = 5;
            XBytes[401] = 6;
#endif
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

        [BenchmarkDotNet.Attributes.Benchmark()]
        public unsafe bool SimpleCompare_32Bit()
        {
            fixed (byte* xPtr = XBytes, yPtr = YBytes)
            {
                return SimpleCompare_32Bit_(xPtr, yPtr, XBytes.Length);
            }
        }

        private unsafe bool SimpleCompare_32Bit_(byte* x, byte* y, int len)
        {
            byte* lastAddr = x + len;
            byte* lastAddrMinus4 = lastAddr - 4;
            while (x < lastAddrMinus4)
            {
                if (*(UInt32*)x != *(UInt32*)y) return false;
                x += 4;
                y += 4;
            }
            while (x < lastAddr)
            {
                if (*x != *y) return false;
                x++;
                y++;
            }

            return true;
        }


        [BenchmarkDotNet.Attributes.Benchmark()]
        public unsafe bool SimpleCompare_64Bit()
        {
            fixed (byte* xPtr = XBytes, yPtr = YBytes)
            {
                return SimpleCompare_64Bit_(xPtr, yPtr, XBytes.Length);
            }
        }

        private unsafe bool SimpleCompare_64Bit_(byte* x, byte* y, int len)
        {
            byte* lastAddr = x + len;
            byte* lastAddrMinus8 = lastAddr - 8;
            while (x < lastAddrMinus8)
            {
                if (*(UInt64*)x != *(UInt64*)y) return false;
                x += 8;
                y += 8;
            }
            while (x < lastAddr)
            {
                if (*x != *y) return false;
                x++;
                y++;
            }

            return true;
        }
#if !NET4_0
        [BenchmarkDotNet.Attributes.Benchmark]
        public bool VectorCompare()
        {
            //Console.WriteLine(Vector.IsHardwareAccelerated);
            if (!Vector.IsHardwareAccelerated)
            {
                return true;
            }
            int offset = Vector<byte>.Count;
            int offsetEnd = XBytes.Length - offset;
            int i = 0;
            for (i = 0; i < offsetEnd; i += offset)
            {
                Vector<byte> xVector = new Vector<byte>(XBytes, i);
                Vector<byte> yVector = new Vector<byte>(YBytes, i);
                if (!xVector.Equals(yVector))
                {
                    return false;
                }
            }
            for (; i < XBytes.Length; i++)
            {
                if (XBytes[i] != YBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        [BenchmarkDotNet.Attributes.Benchmark]
        public bool SequenceCompare()
        {
            return XBytes.SequenceEqual(YBytes);
        }

#if NETCOREAPP2_2_OR_GREATER
        [BenchmarkDotNet.Attributes.Benchmark]
        public unsafe bool SSE2Compare()
        {
            if (Sse2.IsSupported)
            {
                int offset = 128 / 8;
                int offsetEnd = XBytes.Length - offset;
                fixed (byte* xPtr = XBytes, yPtr = YBytes)
                {
                    int i = 0;
                    int mask = 0xffff;
                    for (i = 0; i < offsetEnd; i += offset)
                    {
                        var other = Sse2.CompareEqual(Sse2.LoadVector128(xPtr + i), Sse2.LoadVector128(yPtr + i));
                        if (mask != Sse2.MoveMask(other))
                        {
                            return false;
                        }
                    }

                    for (; i < XBytes.Length; i++)
                    {
                        if (XBytes[i] != YBytes[i])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        [BenchmarkDotNet.Attributes.Benchmark]
        public unsafe bool Avx2Compare()
        {
            if (Avx2.IsSupported)
            {
                int offset = 256 / 8;
                int offsetEnd = XBytes.Length - offset;
                fixed (byte* xPtr = XBytes, yPtr = YBytes)
                {
                    int i = 0;
                    int mask = -1;
                    for (i = 0; i < offsetEnd; i += offset)
                    {
                        var result = Avx2.CompareEqual(Avx2.LoadVector256(xPtr + i), Avx2.LoadVector256(yPtr + i));
                        if (mask != Avx2.MoveMask(result))
                        {
                            return false;
                        }
                    }

                    for (; i < XBytes.Length; i++)
                    {
                        if (XBytes[i] != YBytes[i])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
#endif
#endif
    }
}
