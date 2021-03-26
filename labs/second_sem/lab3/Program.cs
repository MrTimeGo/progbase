using System;
using static System.Console;
using System.IO;

namespace lab3
{
    class Program
    {
        static void GetHelp()
        {
            string[] commands = new string[] { "{set} add {value}", "{set} contains {value}", "{set} remove {value}", "{set} clear",
                "{set} log", "{set} count", "{set} read {filePath}", "{set} write {filePath}", "{set} setEquals", "{set} symmetricExceptWith" };
            WriteLine("List of commands: ");
            foreach (string command in commands)
            {
                WriteLine(command);
            }
        }
        struct CommandParams
        {
            public string command;
            public string set;
            public int intValue;
            public string filePath;
        }
        static CommandParams GetParams(string input)
        {
            string[] subcommands = input.Split(" ");
            CommandParams commandParams = new CommandParams();
            if (subcommands.Length == 2)
            {
                if (subcommands[1] == "clear")
                {
                    commandParams.command = "clear";
                }
                else if (subcommands[1] == "log")
                {
                    commandParams.command = "log";
                }
                else if (subcommands[1] == "count")
                {
                    commandParams.command = "count";
                }
                else if (subcommands[1] == "setEquals")
                {
                    commandParams.command = "setEquals";
                }
                else if (subcommands[1] == "symmetricExceptWith")
                {
                    commandParams.command = "symmetricExceptWith";
                }
                else
                {
                    throw new Exception("Unknown command");
                }
            }
            else if (subcommands.Length == 3)
            {
                if (subcommands[1] == "add")
                {
                    commandParams.command = "add";
                    if (!int.TryParse(subcommands[2], out commandParams.intValue))
                    {
                        throw new Exception("Value is not integer");
                    }
                }
                else if (subcommands[1] == "contains")
                {
                    commandParams.command = "contains";
                    if (!int.TryParse(subcommands[2], out commandParams.intValue))
                    {
                        throw new Exception("Value is not integer");
                    }
                }
                else if (subcommands[1] == "remove")
                {
                    commandParams.command = "remove";
                    if (!int.TryParse(subcommands[2], out commandParams.intValue))
                    {
                        throw new Exception("Value is not integer");
                    }
                }
                else if (subcommands[1] == "read")
                {
                    commandParams.command = "read";
                    if (File.Exists(subcommands[2]))
                    {
                        commandParams.filePath = subcommands[2];
                    }
                    else
                    {
                        throw new Exception("File not found");
                    }
                }
                else if (subcommands[1] == "write")
                {
                    commandParams.command = "write";
                    commandParams.filePath = subcommands[2];
                }
                else
                {
                    throw new Exception("Unknown command");
                }
            }
            else
            {
                throw new Exception("Wrong command length"); 
            }

            if (subcommands[0] == "a")
            {
                commandParams.set = "a";
            }
            else if (subcommands[0] == "b")
            {
                commandParams.set = "b";
            }
            else
            {
                throw new Exception("Wrong set");
            }
            return commandParams;
        }
        static bool Add(ISetInt set, int value)
        {
            return set.Add(value);
        }
        static bool Contains(ISetInt set, int value)
        {
            return set.Contains(value);
        }
        static bool Remove(ISetInt set, int value)
        {
            return set.Remove(value);
        }
        static void Clear(ISetInt set)
        {
            set.Clear();
        }
        static string Log(ISetInt set)
        {
            int[] array = new int[set.Count];
            set.CopyTo(array);
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
                result += i != array.Length - 1 ? "," : ".";
            }
            return result;
        }
        static int Count(ISetInt set)
        {
            return set.Count;
        }
        static void ReadSet(string filePath, ISetInt set)
        {
            StreamReader sr = new StreamReader(filePath);
            string line;
            while (true)
            {
                line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (!int.TryParse(line, out int value))
                {
                    continue;
                }
                set.Add(value);
            }
            sr.Close();
        }
        static void WriteSet(string filePath, ISetInt set)
        {
            int[] array = new int[set.Count];
            set.CopyTo(array);
            StreamWriter sw = new StreamWriter(filePath);
            for (int i = 0; i < array.Length; i++)
            {
                sw.WriteLine(array[i]);
            }
            sw.Close();
        }
        static bool SetEquals(ISetInt set1, ISetInt set2)
        {
            return set1.SetEquals(set2);
        }
        static void SymmetricExceptWith(ISetInt set1, ISetInt set2)
        {
            set1.SymmetricExceptWith(set2);
        }
        static void ProcessSets(ILogger logger)
        {
            SetInt setA = new SetInt();
            SetInt setB = new SetInt();
            bool exit = false;
            while (!exit)
            {
                Write("> ");
                string input = ReadLine();
                if (input == "exit" || input == "")
                {
                    exit = true;
                    continue;
                }
                else if (input == "help")
                {
                    GetHelp();
                    continue;
                }
                CommandParams commandParams;
                try
                {
                    commandParams = GetParams(input);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error: {ex.Message}");
                    continue;
                }
                ISetInt thisSet = commandParams.set == "a" ? setA : setB;
                ISetInt otherSet = commandParams.set == "a" ? setB : setA;
                switch (commandParams.command)
                {
                    case "add":
                        if (Add(thisSet, commandParams.intValue))
                            logger.Log($"Value was added to set {commandParams.set}");
                        else
                            logger.LogError($"Value was not added to set {commandParams.set}");
                        break;
                    case "contains":
                        if (Contains(thisSet, commandParams.intValue))
                            logger.Log($"Value contains in set {commandParams.set}");
                        else
                            logger.LogError($"Value doesn't contain in set {commandParams.set}");
                        break;
                    case "remove":
                        if (Remove(thisSet, commandParams.intValue))
                            logger.Log($"Value was removed from set {commandParams.set}");
                        else
                           logger.LogError($"Value was not removed from set {commandParams.set}");
                        break;
                    case "clear":
                        Clear(thisSet);
                        logger.Log($"Set {commandParams.set} was cleared");
                        break;
                    case "log":
                        logger.Log($"Set {commandParams.set} values: {Log(thisSet)}");
                        break;
                    case "count":
                        logger.Log($"Number of elements in set {commandParams.set}: {Count(thisSet)}");
                        break;
                    case "read":
                        ReadSet(commandParams.filePath, thisSet);
                        logger.Log($"Set {commandParams.set} was updated");
                        break;
                    case "write":
                        WriteSet(commandParams.filePath, thisSet);
                        logger.Log($"Set {commandParams.set} was written to {commandParams.filePath}");
                        break;
                    case "setEquals":
                        logger.Log(SetEquals(thisSet, otherSet) ? "Sets are equal" : "Sets are not equal");
                        break;
                    case "symmetricExceptWith":
                        SymmetricExceptWith(thisSet, otherSet);
                        logger.Log($"Set {commandParams.set} was updated");
                        break;
                }
            }
        }
        struct Options
        {
            public string loggerType;
            public string filePath;
            public string errors;
        }
        static Options ParseOptions(string[] args)
        {
            if (args.Length == 0 || (args.Length == 1 && args[0] == "console"))
            {
                return new Options() { loggerType = "console" };
            }
            else if (args.Length == 1)
            {
                return new Options() { loggerType = "csv", filePath = args[0] };
            }
            else
            {
                return new Options() { errors = "Unknown logger type" };
            }
        }
        static void Main(string[] args)
        {
            Options options = ParseOptions(args);
            if (options.errors != null)
            {
                WriteLine(options.errors);
                return;
            }
            
            if (options.loggerType == "console")
            {
                ConsoleLogger logger = new ConsoleLogger();
                ProcessSets(logger);
            }
            else
            {
                CsvFileLogger logger = new CsvFileLogger(options.filePath);
                ProcessSets(logger);
                logger.Close();
            }
        }
    }
}
