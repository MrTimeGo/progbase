using System;
using static System.Console;

namespace task3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter score: ");
            int score = int.Parse(ReadLine());

            if (score < 60)
            {
                WriteLine("F");
            }
            else if (score <= 64)
            {
                WriteLine("E");
            }
            else if (score <= 74)
            {
                WriteLine("D");
            }
            else if (score <= 84)
            {
                WriteLine("C");
            }
            else if (score <= 94)
            {
                WriteLine("B");
            }
            else
            {
                WriteLine("A");
            }
        }
    }
}
