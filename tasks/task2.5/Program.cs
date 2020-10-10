using System;
using static System.Console;
using static System.Math;

namespace task2._5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter x: ");
            double x = double.Parse(ReadLine());
            Console.Write("Enter y: ");
            double y = double.Parse(ReadLine());
            Console.Write("Enter z: ");
            double z = double.Parse(ReadLine());

            double a0;
            a0 = (Pow(x, y+1))/Pow((x-y), 1/z);

            double a1;
            a1 = (1*y) + z/x;

            double a2;
            a2 = Sqrt(Abs(Cos(y)/Sin(x) + 2));

            WriteLine("a0: " + a0);
            WriteLine("a1: " + a1);  
            WriteLine("a2: " + a2);

            WriteLine("sum: " + a0+a1+a2);    
        }
    }
}
