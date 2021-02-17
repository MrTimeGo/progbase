using System;
using static System.Console;
using static System.Math;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("---------------------------------");
            WriteLine("Values of y for x in range [-10; 10]:");
            for(double x = -10; x <= 10; x += 0.5)
            {
                WriteLine();
                WriteLine("y({0}) = {1}", x, Fx(x));
            }
            WriteLine("---------------------------------");

            Write("Enter xMin: ");
            double xMin = double.Parse(ReadLine());
            Write("Enter xMax: ");
            double xMax = double.Parse(ReadLine());
            Write("Enter nStep: ");
            int nStep = int.Parse(ReadLine());

            int ErrorCode = CanInt(xMin, xMax, nStep);
            if (ErrorCode == 0)
            {
                WriteLine("Integral for f(x) in range [{0}; {1}] is: ", xMin, xMax);
                WriteLine(IntX(xMin, xMax, nStep));
            }
            else if (ErrorCode == -1)
            {
                WriteLine("Cannot calculate integral, range is invalid");
            }
            else if(ErrorCode == -2)
            {
                WriteLine("Cannot calculate integral, there is an invalid number in range");
            }
            else
            {
                WriteLine("Cannot calculate integral, nStep is not positive");
            }
        }
        static double Fx(double x)
        {
            if ((x < -1 && x >= -4.5) || (x > 1 && x <= 4.5))
            {
                return Gx(x);
            }
            else if (x == 0)
            {
                return double.NaN;
            }
            else
            {
                return Hx(x);
            }
        }
        static double Gx(double x)
        {
            return 3 * Sin(2 * x) - 2/x;
        }
        static double Hx(double x)
        {
            return 1.3 * (x + 1) - 2/x;
        }
        static double IntX(double xMin, double xMax, double nStep)
        {
            double xStep = (xMax- xMin)/(double)nStep;
            double sum = 0;
            for(double x = xMin; x < xMax; x += xStep)
            {
                sum += Fx(x) * xStep;
            }
            return sum;
        }
        static int CanInt(double xMin, double xMax, double nStep)
        {
            if (xMin > xMax)
            {
                return -1;
            }
            else if (xMin <= 0 && xMax >= 0)
            {
                return -2;
            }
            else if (nStep <= 0)
            {
                return -3;
            }
            else
            {
                return 0;
            }
        }
    }
}