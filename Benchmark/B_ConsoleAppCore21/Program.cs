﻿using ClassLibrary4_0;
using System;

namespace B_ConsoleAppCore21
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
