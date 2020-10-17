using System;
using static System.Console;

namespace task3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter 3 sides of triangle: ");
            Write("a: ");
            int a = int.Parse(ReadLine());
            Write("b: ");
            int b = int.Parse(ReadLine());
            Write("c: ");
            int c = int.Parse(ReadLine());

            if (c < a + b && a < b + c && b < a + c)
            {
                if (a == b && a == c)
                {
                    WriteLine("This triangle is equilateral.");
                }
                else if (a == b || a == c || b == c)
                {
                    WriteLine("This triangle is isosceles.");
                }
                else
                {
                    WriteLine("This triangle is arbitrary.");
                }
                
            }
            else
            {
                WriteLine("This triangle does not exist.");
            }
        }
    }
}
