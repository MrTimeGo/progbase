using System;
using static System.Console;
using static System.Math;

namespace task5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Clear();
            double[] input = new double[8];
            for (int i = 0; i < 8; i++)
            {
                Write("{0} element: ", i);
                input[i] = double.Parse(ReadLine());
            }
            Clear();

            WriteLine("Entered massive: ");
            Print_array(input);

            double[] new_array1 = new double[8];
            double temp = input[7];
            for (int i = 0; i < 7; i++)
            {
                new_array1[i+1] = input[i];
            }
            new_array1[0] = temp;

            WriteLine("First: ");
            Print_array(new_array1);

            double[] new_array2 = new double[8];
            double temp1 = new_array1[0];
            double temp2 = new_array1[1];
            for (int i = 0; i < 6; i++)
            {
                new_array2[i] = new_array1[i + 2];
            }
            new_array2[6] = temp1;
            new_array2[7] = temp2;

            WriteLine("Second: ");
            Print_array(new_array2);

            double[] new_array3 = new double[8];
            for (int i = 0; i < 8; i++)
            {
                if (new_array2[i] < 0)
                {
                    new_array3[i] = 0;
                }
                else
                {
                    new_array3[i] = new_array2[i];
                }
            }

            WriteLine("Third: ");
            Print_array(new_array3);
        }
        static void Print_array(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Write(array[i] + " ");
            }
            WriteLine();
        }
    }
}
