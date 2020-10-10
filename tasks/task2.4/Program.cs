using System;

namespace task2._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Enter b: ");
            int b = int.Parse(Console.ReadLine());

            Console.Write("Enter c: ");
            int c = int.Parse(Console.ReadLine());

            int ab_max = Math.Max(a, b);
            int ab_min = Math.Min(a, b);
            Console.WriteLine("Max: " + Math.Max(ab_max, c));
            Console.WriteLine("Min: " + Math.Min(ab_min, c));
        }
    }
}
