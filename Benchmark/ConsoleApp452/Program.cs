using B_ClassLibrary;
using System;
using System.Collections.Generic;
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

            BenchmarkDotNet.Running.BenchmarkRunner.Run<SIMDTest>();

            Console.ReadKey();
        }
    }
}
