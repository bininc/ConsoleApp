using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Fibonacci
    {
        public static IEnumerable<int> GenerateFibonacci(int n)
        {
            int current = 1, next = 1;

            for (int i = 0; i < n; i++)
            {
                yield return current;
                next = current + (current = next);
            }
        }

        public static IEnumerable<int> Fibs()
        {
            var (x, y) = (1, 1);
            yield return x;
            yield return y;
            while (y < 100)
            {
                (x, y) = (y, x + y);
                yield return y;
            }
        }

        public static IEnumerable<int> GenerateFibonacci2(int n) { 
            if(n>=1) yield return 1;

            int a = 1, b = 0;
            for (int i = 2; i <= n; i++)
            {
                int t = b;
                b = a;
                a += t;
                yield return a;
            }
        }
    }
}
