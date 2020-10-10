using System;

namespace task2._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter x: ");
            int x = int.Parse(Console.ReadLine());

            double f1;
            f1 = Math.Pow(x, 2) + Math.Sin(x);

            double f2;
            f2 = Math.Sqrt(Math.Pow(Math.Cos(x), 2) + Math.Abs(x));

            double f3;
            f3 = 1.0/(x+3) - (Math.Pow(x, 2) +50)/2.0;

            Console.WriteLine("First: " + f1);
            Console.WriteLine("Second: " + f2);
            Console.WriteLine("Third: " + f3);

        }
    }
}
