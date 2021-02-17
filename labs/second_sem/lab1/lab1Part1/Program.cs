using System;
using static System.Console;
using static System.IO.File;

namespace lab1
{
    class StringBuilder
    {
        private string[] strings;
        private int size;

        public StringBuilder()
        {
            strings = new string[16];
            size = 0;
        }
        public StringBuilder Append(string str)
        {
            if (strings.Length == size)
            {
                Expand();
            }
            strings[size] = str;
            size += 1;
            return this;
        }
        private void Expand()
        {
            int oldCapacity = strings.Length;
            string[] oldArray = strings;
            strings = new string[oldCapacity * 2];
            Array.Copy(oldArray, strings, oldCapacity);
        }
        private int GetTotalLength()
        {
            int charCounter = 0;
            for (int i = 0; i < size; i++)
            {
                string str = strings[i];
                charCounter += str.Length;
            }
            return charCounter;
        }
        public override string ToString()
        {
            int charCounter = GetTotalLength();
            char[] buffer = new char[charCounter];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                string str = strings[i];
                Array.Copy(str.ToCharArray(), 0, buffer, index, str.Length);
                index += str.Length;
            }
            string allStrings = new string(buffer);
            return allStrings;
        }
    }

    class Program
    {
        struct Options
        {
            public string parsingError;
            public string outputFile;
            public int numberOfCapitals;
        }
        static Options ParseOptions(string[] args)
        {
            Options options = new Options();
            if (args.Length < 2)
            {
                options.parsingError = "Not enough arguments";
            }
            else if (args.Length > 2)
            {
                options.parsingError = "Too much arguments";
            }
            else if (!int.TryParse(args[1], out options.numberOfCapitals))
            {
                options.parsingError = "The third argument is not a number";
            }
            else if (options.numberOfCapitals < 0)
            {
                options.parsingError = "Number is negative";
            }
            else
            {
                options.outputFile = args[0];
            }
            return options;
        }
        struct Capital
        {
            public int id;
            public string name;
            public string country;
            public int population;
            public double area;
        }
        static Capital[] GenerateCapitals(int N)
        {
            Capital[] capitals = new Capital[N];
            Random rand = new Random();
            string[] namesPattern = new string[] { "Canberra", "Vienna", "Baku", "Manama", "Sucre", "La Paz", "Brasília", "Sofia", "Ouagadougou", "Ottawa", "West Island",
            "Moroni", "Prague", "Copenhagen", "Hanga Roa", "Asmara", "Helsinki", "Papeete", "Tbilisi", "Conakry", "New Delhi", "Jerusalem"};
            string[] countryPattern = new string[] {"Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia Herzegovina",
            "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Central African Rep" };

            for (int i = 0; i < capitals.Length; i++)
            {
                capitals[i].id = i;
                capitals[i].name = namesPattern[rand.Next(namesPattern.Length)];
                capitals[i].country = countryPattern[rand.Next(countryPattern.Length)];
                capitals[i].population = rand.Next(1000000);
                capitals[i].area = Math.Round(rand.Next(1000) * rand.NextDouble(), 3);
            }
            return capitals;
        }
        static string MakeCsvString(Capital[] capitals)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("id;name;country;population;area\n");
            for (int i = 0; i < capitals.Length; i++)
            {
                sb.Append(capitals[i].id.ToString()).Append(";").Append(capitals[i].name).Append(";").Append(capitals[i].country).Append(";").Append(capitals[i].population.ToString()).Append(";").Append(capitals[i].area.ToString());
                if (i != capitals.Length - 1)
                {
                     sb.Append("\n");
                }
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {
            Options options = ParseOptions(args);
            if (options.parsingError != null)
            {
                WriteLine("Parsing error: {0}", options.parsingError);
                return;
            }
            Capital[] capitals = GenerateCapitals(options.numberOfCapitals);
            string csvString = MakeCsvString(capitals);
            WriteAllText(options.outputFile, csvString);
            WriteLine("Program has genereted {0} capitals succesfully", options.numberOfCapitals);
        }
    }
}
