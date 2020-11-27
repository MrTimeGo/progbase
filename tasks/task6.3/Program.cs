using System;
using static System.Console;
using static System.IO.File;

namespace task6._3
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = ReadAllText("./data.txt");

            WriteLine("Paragraphs: ");
            string[] paragraphs = text.Split("\n");
            PrintArray(paragraphs);

            WriteLine();
            WriteLine("------------");
            WriteLine();

            WriteLine("Words: ");
            string[] words = text.Split(" ");
            PrintArray(words);
        }
        static void PrintArray(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                WriteLine(array[i]);
                WriteLine("-------");
            }
        }
    }
}
