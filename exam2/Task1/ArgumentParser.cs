using System;
using System.Collections.Generic;
using static System.Console;

namespace Task1
{
    static class ArgumentParser
    {
        public static void Run(string[] subcommands)
        {
            ValidateCommandLength(subcommands.Length);
            switch (subcommands[0])
            {
                case "gen_num": ProcessGenNum(subcommands); break;
                case "num": ProcessNum(subcommands); break;
                case "num_uni": ProcessNumUni(subcommands); break;
                case "gen_vec": ProcessGenVec(subcommands); break;
                case "vec": ProcessVec(subcommands); break;
            }
        }
        private static void ProcessVec(string[] subcommands)
        {
            if (subcommands.Length != 2)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];

            Vector[] array = DataIO.ReadFromXml(f);
            int count = DataProcessor.NumberOfSpecVec(array);
            WriteLine($"Number of vecs with one abs cordinate is prime: {count}");
        }
        private static void ProcessGenVec(string[] subcommands)
        {
            if (subcommands.Length != 5)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];
            int a = GetNum(subcommands[2]);
            int b = GetNum(subcommands[3]);
            int n = GetNum(subcommands[4]);

            Vector[] array = DataProcessor.GenVec(a, b, n);
            DataIO.WriteToXml(f, array);
            WriteLine($"Vectors was written to {f}");
        }
        private static void ProcessNumUni(string[] subcommands)
        {
            if (subcommands.Length != 3)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];
            string fout = subcommands[2];

            int[] inputArray = DataIO.ReadFromFile(f);
            int[] outputArray = DataProcessor.ReadUnique(inputArray);
            DataIO.WriteToFile(fout, outputArray);
            WriteLine("Unique nums was rewritten");
        }
        private static void ProcessNum(string[] subcommands)
        {
            if (subcommands.Length != 2)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];

            int[] array = DataIO.ReadFromFile(f);
            double avarage = DataProcessor.GetAverageOfNegative(array);
            WriteLine($"Average of negative: {avarage}");
        }
        private static void ProcessGenNum(string[] subcommands)
        {
            if (subcommands.Length != 5)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];
            int a = GetNum(subcommands[2]);
            int b = GetNum(subcommands[3]);
            int n = GetNum(subcommands[4]);

            int[] array = DataProcessor.GenNum(n, a, b);
            DataIO.WriteToFile(f, array);
            WriteLine($"Nums was written to {f}");
        }
        private static int GetNum(string str)
        {
            if (!int.TryParse(str, out int num))
            {
                throw new ArgumentException("Not integer");
            }
            return num;
        }
        private static void ValidateCommandLength(int length)
        {
            if (length < 2)
            {
                throw new ArgumentException("Command length should be more than 2.");
            }
        }
    }
}
