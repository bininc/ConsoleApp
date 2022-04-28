using B_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp452
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8 };
            //Console.WriteLine(string.Join(",",arr));
            //foreach (ref int i in arr.AsSpan())
            //{
            //    i++;
            //}
            //Console.WriteLine(string.Join(",", arr));
            SIMDTest test = new SIMDTest();
            BenchmarkDotNet.Running.BenchmarkRunner.Run(test.GetType());
            Stopwatch sw = Stopwatch.StartNew();
            bool suc = test.VectorCompare();
            sw.Stop();
            Console.WriteLine($"result:{suc} time:{sw.ElapsedTicks}");
      
            Console.ReadKey();
        }
    }
}
