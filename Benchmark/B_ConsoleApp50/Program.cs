using ClassLibrary4_0;
using System;

namespace B_ConsoleApp50
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<SIMDTest>();
            Console.ReadLine();
        }
    }
}
