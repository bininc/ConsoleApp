using B_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SIMDTest test = new SIMDTest();
            BenchmarkDotNet.Running.BenchmarkRunner.Run(test.GetType());
            Stopwatch sw = Stopwatch.StartNew();
            bool suc = test.VectorCompare();
            sw.Stop();
            Console.WriteLine($"result:{suc} time:{sw.ElapsedTicks}");
            Console.ReadLine();
        }
    }
}
