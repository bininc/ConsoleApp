// See https://aka.ms/new-console-template for more information
using B_ClassLibrary;
using System.Diagnostics;

SIMDTest test = new SIMDTest();
#if RELEASE
BenchmarkDotNet.Running.BenchmarkRunner.Run(test.GetType());
#endif
Stopwatch sw = Stopwatch.StartNew();
bool suc = test.Avx2Compare();
sw.Stop();
Console.WriteLine($"result:{suc} time:{sw.ElapsedTicks}");

Console.ReadLine();
