using System;
using static System.Math;
using static System.Console;

namespace task4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int j = 2;
            bool Breaker;
            int sum = 0;
            for (int i = 100; i <= 200; i++)
            {
                Breaker = false;
                j = 2;
                while (j <= Sqrt(i) && Breaker == false)
                {
                    if (i % j == 0)
                    {
                        Breaker = true;
                    }
                    j++;
                }
                if (Breaker == false)
                {
                    sum = sum + i;
                    WriteLine("Prime: " + i);
                }
            }
            WriteLine("Sum is: " + sum);
        }
    }
}
