using System;
using static System.Console;
using static System.IO.File;

namespace lab5
{
    class Program
    {
        struct Capital
        {
            public int id;
            public string name;
            public string country;
            public double population;
            public int square;
        }
        static string task2String = "";
        static string task3CsvText = "";
        static string[,] task3Table = new string[0,0];
        static Capital[] task3Capitals = new Capital[0];
        static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                Write("Enter command: ");
                string command = ReadLine();
                string[] subcommand = command.Split(':');
                if (subcommand[0] == "char")
                    ProcessChar(command);
                else if (subcommand[0] == "string")
                    ProcessString(command);
                else if (subcommand[0] == "csv")
                    ProcessCsv(command);
                else if (subcommand[0] == "exit")
                    exit = true;
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find this command.", command);
            } while (exit == false);
            WriteLine("Bye-bye!");
        }
        static void ProcessChar (string command)
        {
            string[] subcommand = command.Split(":");
            if (subcommand.Length == 2)
            {
                if (subcommand[1] == "all")
                    ProcessCharAll();
                else if (subcommand[1] == "upper")
                    ProcessCharUpper();
                else if (subcommand[1] == "alpha")
                    ProcessCharAlpha();
                else if (subcommand[1] == "alnum")
                    ProcessCharAlnum();
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find argument.", command);
            }
            else
            {
                WriteLine("Entered command '{0}' is invalid. Invalid arguments.", command);
            }
        }
        static void ProcessCharAll()
        {
            WriteLine("Full ASCII table:");
            for (int i = 0; i < 128; i++)
            {
                WriteLine("Code: {0} - Char: '{1}'", i, (char)i);
            }
        }
        static void ProcessCharUpper()
        {
            WriteLine("ASCII upper letters:");
            for (int i = 65; i < 91; i++)
            {
                WriteLine("Code: {0} - Char: '{1}'", i, (char)i);
            }
        }
        static void ProcessCharAlpha()
        {
            WriteLine("ASCII alphabet:");
            for (int i = 65; i < 91; i++)
            {
                WriteLine("Code: {0} - Char: '{1}'", i, (char)i);
                WriteLine("Code: {0} - Char: '{1}'", i+32, (char)(i+32));
            }
        }
        static void ProcessCharAlnum()
        {
            WriteLine("ASCII letters and nums:");
            for (int i = 48; i < 58; i++)
            {
                WriteLine("Code: {0} - Char: '{1}'", i, (char)i);
            }
            for (int i = 65; i < 91; i++)
            {
                WriteLine("Code: {0} - Char: '{1}'", i, (char)i);
                WriteLine("Code: {0} - Char: '{1}'", i+32, (char)(i+32));
            }
        }
        static void ProcessString (string command)
        {
            string[] subcommand = command.Split(':');
            if (subcommand.Length == 2)
            {   
                if (subcommand[1] == "print")
                    ProcessStringPrint();
                else if (subcommand[1] == "upper")
                    ProcessStringUpper();
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find argument.", command);
            }
            else if (subcommand.Length == 3)
            {
                if (subcommand[1] == "set")
                    ProcessStringSet(subcommand[2]);
                else if (subcommand[1] == "contains")
                    ProcessStringContains(subcommand[2]);
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find argument.", command);
            }
            else if (subcommand.Length == 4)
            {
                if (subcommand[1] == "substr")
                {
                    int start_index;
                    int lenth;
                    if(int.TryParse(subcommand[2], out start_index) && int.TryParse(subcommand[3], out lenth))
                        ProcessStringSubstr(start_index, lenth);
                    else
                        WriteLine("Entered command '{0}' is invalid. Arguments must be integers", command);
                }
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find argument.", command);
            }
            else
                WriteLine("Entered command '{0}' is invalid. Invalid arguments.", command);
        }
        static void ProcessStringPrint()
        {
            WriteLine("String: '{0}'", task2String);
        }
        static void ProcessStringSet(string NewString)
        {
            task2String = NewString;
        }
        static void ProcessStringSubstr(int start_index, int lenth)
        {
            if (task2String.Length < lenth)
                WriteLine("String: '{0}'", task2String.Substring(start_index));
            else
                WriteLine("Part of string: '{0}'", task2String.Substring(start_index, lenth));
        }
        static void ProcessStringUpper()
        {
            WriteLine("String: '{0}'", task2String.ToUpper());
        }
        static void ProcessStringContains(string CheckStr)
        {
            if (task2String.Contains(CheckStr))
                WriteLine("True");
            else
                WriteLine("False");
        }
        static void ProcessCsv (string command)
        {
            string[] subcommand = command.Split(':');
            if (subcommand.Length == 2)
            {
                if (subcommand[1] == "load")
                    ProcessCsvLoad();
                else if (subcommand[1] == "text")
                    ProcessCsvText();
                else if (subcommand[1] == "table")
                    ProcessCsvTable();
                else if (subcommand[1] == "capitals")
                    ProcessCsvCapitals();
                else if (subcommand[1] == "save")
                    ProcessCsvSave();
                else
                    WriteLine("Entered command '{0}' is invalid. Can't find argument.", command);
            }
            else if (subcommand.Length == 3)
                if (subcommand[1] == "get")
                {
                    int index;
                    if (int.TryParse(subcommand[2], out index) && index <= task3Capitals.Length)
                        ProcessCsvGet(index);
                    else
                        WriteLine("Entered command '{0}' is invalid. Invalid index", command);
                }
                else
                    WriteLine("Entered command '{0}' is invalid. Invalid arguments.", command);
            else if (subcommand.Length == 5)
            {
                if (subcommand[1] == "set")
                {
                    int index;
                    if (int.TryParse(subcommand[2], out index) && index <= task3Capitals.Length)
                        ProcessCsvSet(index, subcommand[3], subcommand[4]);
                    else
                        WriteLine("Entered command '{0}' is invalid. Invalid index", command);
                }
                else
                    WriteLine("Entered command '{0}' is invalid. Invalid arguments.", command);
            }
            else
                WriteLine("Entered command '{0}' is invalid. Invalid arguments.", command);
        }
        static string[,] CsvToTable(string csvText)
        {
            string[] rows = csvText.Split("\r\n");
            string[,] capitals = new string[rows.Length, 5];
            for (int i = 0; i < rows.Length; i++)
            {
                string[] cols = rows[i].Split(',');
                for (int j = 0; j < cols.Length; j++)
                {
                    capitals[i,j] = cols[j];
                }
            }
            return capitals;
        }
        static Capital[] TableToCapitals(string[,] csvTable)
        {
            int length = csvTable.GetLength(0);
            Capital[] capitals = new Capital[length];
            for (int i = 0; i < length; i++)
            {
                capitals[i] = new Capital {id = int.Parse(csvTable[i, 0]), name = csvTable[i, 1], country = csvTable[i, 2], population = double.Parse(csvTable[i, 3]), square = int.Parse(csvTable[i,4])};
            }
            return capitals;
        }
        static string[,] CapitalsToTable(Capital[] capitals)
        {
            string[,] csvTable = new string[capitals.Length, 5];
            for (int i = 0; i < capitals.Length; i++)
            {
                csvTable[i, 0] = Convert.ToString(capitals[i].id);
                csvTable[i, 1] = capitals[i].name;
                csvTable[i, 2] = capitals[i].country;
                csvTable[i, 3] = Convert.ToString(capitals[i].population);
                csvTable[i, 4] = Convert.ToString(capitals[i].square);
            }
            return csvTable;
        }
        static string TableToCsv(string[,] csvTable)
        {
            string csvText = "";
            int num_cols = csvTable.GetLength(1);
            for (int i = 0; i < csvTable.GetLength(0); i++)
            {
                for (int j = 0; j < num_cols - 1; j++)
                {
                    csvText = csvText + csvTable[i,j] + ",";
                }
                if (i != csvTable.GetLength(0) - 1)
                    csvText = csvText + csvTable[i,num_cols - 1] + "\r\n";
                else
                    csvText = csvText + csvTable[i,num_cols - 1];
            }
            return csvText;
        }
        static void ProcessCsvLoad()
        {
            task3CsvText = ReadAllText("./data.csv");
            task3Table = CsvToTable(task3CsvText);
            task3Capitals = TableToCapitals(task3Table);
        }
        static void ProcessCsvText()
        {
            WriteLine("CSV text:");
            WriteLine(task3CsvText);
        }
        static void ProcessCsvTable()
        {
            WriteLine("CSV Table: ");
            WriteLine("{0,3} | {1,20} | {2,20} | {3,20} | {4,20}", "id", "name", "country", "population(mln)", "square(km^2)");
            for (int i = 0; i <= 94; i++)
                Write("-");
            WriteLine();
            for (int i = 0; i < task3Table.GetLength(0); i++)
            {
                WriteLine("{0,3} | {1,20} | {2,20} | {3,20} | {4,20}", task3Table[i,0], task3Table[i,1], task3Table[i,2], task3Table[i,3], task3Table[i,4]);
            }
        }
        static void ProcessCsvCapitals()
        {
            WriteLine("Capitals: ");
            int length = task3Capitals.Length;
            for (int i = 0; i < length - 1; i++)
            {
                Write("{0}, ", task3Capitals[i].name);
            }
            WriteLine(task3Capitals[length - 1].name + ".");
        }
        static void ProcessCsvGet(int index)
        {
            if (task3Capitals.Length >= index)
            {
                Capital capital = task3Capitals[index - 1];
                WriteLine("Data for capital:");
                WriteLine("{0}. {1} is the capital of {2}. The population is {3} million people. Square is {4} km^2.", capital.id, capital.name, capital.country, capital.population, capital.square);
            }
        }
        static void ProcessCsvSet(int index, string field, string value)
        {
            if (task3Capitals.Length >= index)
            {
                Capital capital = task3Capitals[index - 1];
                if (field == "id")
                    capital.id = int.Parse(value);
                else if (field == "name")
                    capital.name = value;
                else if (field == "country")
                    capital.country = value;
                else if (field == "population")
                    capital.population = double.Parse(value);
                else if (field == "square")
                    capital.square = int.Parse(value);
                task3Capitals[index - 1] = capital;
                task3Table = CapitalsToTable(task3Capitals);
                task3CsvText = TableToCsv(task3Table);
            }
        }
        static void ProcessCsvSave()
        {
            WriteAllText("./data.csv", task3CsvText);
            task3CsvText = "";
            task3Table = new string[0,0];
            task3Capitals = new Capital[0];
        }
    }
}