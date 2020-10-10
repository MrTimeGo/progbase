using System;
using static System.Console;
using static System.Math;

class Program
{
    static void Main()
    {
        WriteLine("Part1");
        Write("Enter a: ");
        double a = double.Parse(ReadLine());

        Write("Enter b: ");
        double b = double.Parse(ReadLine());
        
        Write("Enter c: ");
        double c = double.Parse(ReadLine());

        WriteLine("---------------------------");


        if (a == b || a == -b || c == 0 || (a == -3 && c < -1))
        {
            WriteLine("Entered numbers are out of range of valid values");
        }
        else
        {
            double d0 = (Pow(a + 3, c + 1) - 10)/(a - b);
            
            double d1 = b/(5* Abs(a + b));

            double d2 = Pow(3 + Sin(b), Cos(a)/c);

            double d = d0 + d1 + d2;

            WriteLine("d0: {0}", d0);
            WriteLine("d1: {0}", d1);
            WriteLine("d2: {0}", d2);
            WriteLine("d: {0}", d);
        }


        WriteLine("***************************");
        WriteLine("Part 2");

        Write("Enter x: ");
        double x = double.Parse(ReadLine());

        double y;

        if (x == 0)
        {
            y = double.NaN;
        }
        else if (x > -3 && x <= 3)
        {
            y = 3 * Sin(2 * x) - 2/x;
        }
        else
        {
            y = 1.3 * (x + 1) - 2/x;
        }
        
        WriteLine("---------------------------");

        WriteLine("y: {0}", y);
    }
}

