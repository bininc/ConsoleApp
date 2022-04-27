// See https://aka.ms/new-console-template for more information
using B_ClassLibrary;
using System.Diagnostics;

SIMDTest test = new SIMDTest();
BenchmarkDotNet.Running.BenchmarkRunner.Run(test.GetType());
Stopwatch sw = Stopwatch.StartNew();
bool suc = test.SimpleCompare();
sw.Stop();
Console.WriteLine($"result:{suc} time:{sw.ElapsedTicks}");

Console.ReadLine();
