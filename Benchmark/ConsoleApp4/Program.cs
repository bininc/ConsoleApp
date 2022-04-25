using ClassLibrary4_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var item in GenerateFibonacci(10))
            //{
            //    Console.WriteLine(item);
            //}

            //int[] arr = { 1, 2, 3, 4, 5, 6 };
            //Console.WriteLine(string.Join(",",arr));

            BenchmarkDotNet.Running.BenchmarkRunner.Run<SIMDTest>();

            Console.ReadLine();
        }

        public static IEnumerable<int> GenerateFibonacci(int n)
        {
            int current = 1, next = 1;
            for (int i = 0; i < n; i++)
            {
                yield return current;
                next = current + (current = next);
            }
        }
    }
}
