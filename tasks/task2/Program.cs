using System;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            Console.Write("Enter a: ");
            a = int.Parse(Console.ReadLine());

            int b;
            Console.Write("Enter b: ");
            b = int.Parse(Console.ReadLine());

            int c;
            Console.Write("Enter c: ");
            c = int.Parse(Console.ReadLine());

            int sum;
            sum = a + b + c;

            double average;
            average = sum/3.0;

            Console.WriteLine("Average: {0}", average);
        }
    }
}
