using System;
using static System.Console;
using static System.IO.File;

namespace task6._4
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = ReadAllText("./data.csv");
            string[] lines = text.Split("\n");
            PrintArray(lines);

            WriteLine();
            WriteLine();
            WriteLine();

            foreach (string line in lines)
            {
                string[] obj = line.Split(",");
                PrintArray(obj);
                WriteLine("*********");
            }
        }
        static void PrintArray(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                WriteLine(array[i]);
                WriteLine("--------");
            }
        }
    }
}
