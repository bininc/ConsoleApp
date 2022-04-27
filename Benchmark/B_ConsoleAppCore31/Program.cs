using System;
using B_ClassLibrary;

namespace B_ConsoleAppCore31
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
