using System;
using static System.Console;
using static System.Math;

namespace task3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter x: ");
            double x = double.Parse(ReadLine());

            double y;

            if (x >=0)
            {
                y = Sin(Pow(x, 3));
            }
            else
            {
                y = Sin(x - PI);
            }

            WriteLine("y: " + y);

        }
    }
}
