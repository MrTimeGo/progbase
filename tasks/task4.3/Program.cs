using System;
using static System.Console;
using static System.Math;

namespace task4._3
{
    class Program
    {
        static void Main(string[] args)
        {
            double xMin = -10;
            double xMax = 10;
            double xStep = 0.2;
            double x = xMin;
            double y;
            double min = double.MaxValue, max = double.MinValue;
            while (x <= xMax)
            {
                if (x <= 0 )
                {
                    y = -5 * Sqrt(-x);
                }
                else
                {
                    y = Pow(0.4*x, 2) + 5*Sin(x);
                }
                if (min > y)
                {
                    min = y;
                }
                if (max < y)
                {
                    max = y;
                }
                x = x + xStep;
            }
            WriteLine("Max: {0}, Max: {1}", max, min);
        }
    }
}
