using System;
using static System.Console;
using static System.Math;

namespace task4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            double num, sum = 0, average, sum_odd = 0, sum_even = 0, min = 0, max = 0;
            int counter = 0;
            do
            {
                num = double.Parse(ReadLine());
                counter++;
                sum = sum + num;
                if (min > num)
                {
                    min = num;
                }
                if (max < num)
                {
                    max = num;
                }
                if (num % 2 == 0)
                {
                    sum_even = sum_even + num;
                }
                else
                {
                    sum_odd = sum_odd + num;
                }
            } while(num != 0);
            average = sum / counter;
            WriteLine("Sum: {0}, Average: {1}, Sum of Odd: {2}, Sum of Even: {3}, Max: {4}, Min: {5}", sum, average, sum_odd, sum_even, max, min);
        }
    }
}
