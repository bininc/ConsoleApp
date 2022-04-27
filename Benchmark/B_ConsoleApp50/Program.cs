using B_ClassLibrary;
using System;
using System.Diagnostics;

namespace B_ConsoleApp50
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SIMDTest test = new SIMDTest();
            BenchmarkDotNet.Running.BenchmarkRunner.Run(test.GetType());
            Stopwatch sw = Stopwatch.StartNew();
            bool suc = test.SimpleCompare();
            sw.Stop();
            Console.WriteLine($"result:{suc} time:{sw.ElapsedTicks}");

            Console.ReadLine();
        }
    }
}
