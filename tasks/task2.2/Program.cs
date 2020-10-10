using System;

namespace task2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter num: ");
            int num = int.Parse(Console.ReadLine());

            int digit1 = num / 100;
            int digit2 = (num / 10) % 10;
            int digit3 = num % 10;
            
            int sum = digit1 + digit2 + digit3;
            Console.WriteLine("Sum: {0}", sum);
        }
    }
}
