using System;
using System.Diagnostics;

namespace task7
{
    class Program
    {
        struct Option
        {
            public bool isInteractiveMode;  // for -i boolean option
            public string inputFile;  // for the independent option
            public string outputFile;  // for -o value option
            // for errors
            public string parsingError;
            public string splitString;
        }
        static void Main()
        {
            string[] input1 = new string[0];
            Option output1 = ParseOption(input1);
            Option expected1 = new Option
                                {
                                    isInteractiveMode = false,
                                    inputFile = "",
                                    outputFile = "",
                                    parsingError = "",
                                    splitString = ""
                                };
            Debug.Assert(CompareOptions(output1, expected1), "empty args");

            string[] input2 = new string[] {"-s", "\"Hello world\""};
            Option output2 = ParseOption(input2);
            Option expected2 = new Option
                                {
                                    isInteractiveMode = false,
                                    inputFile = "",
                                    outputFile = "",
                                    parsingError = "",
                                    splitString = "Hello world"
                                };
            Debug.Assert(CompareOptions(output2, expected2));

            string[] input3 = new string[] {"-s", "\"string\"", "something strange"};
            Option output3 = ParseOption(input3);
            Debug.Assert(output3.parsingError.Length > 0);
        }
        static bool CompareOptions(Option opt1, Option opt2)
        {
            return opt1.isInteractiveMode == opt2.isInteractiveMode
                    && opt1.inputFile == opt2.inputFile
                    && opt1.outputFile == opt2.outputFile
                    && opt1.parsingError == opt2.parsingError
                    && opt1.splitString == opt2.splitString;
        }
        static Option ParseOption(string[] args)
        {
            bool[] IsParsedArr = new bool[args.Length];
            bool hasUnused = false;

            Option returnOption = new Option
                                    {
                                        isInteractiveMode = false,
                                        inputFile = "",
                                        outputFile = "",
                                        parsingError = "",
                                        splitString = ""
                                    };
            if (args.Length == 0)
            {
                return returnOption;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s")
                {
                    if (i == args.Length - 1 || args[i+1][0] == '-' || !(args[i+1][0] == '\"' && args[i+1][args[i+1].Length - 1] == '\"'))
                    {
                        return new Option
                            {
                                isInteractiveMode = false,
                                inputFile = "",
                                outputFile = "",
                                parsingError = "option -s has no value",
                                splitString = ""
                            };
                    }
                    returnOption.splitString = args[i+1];
                    IsParsedArr[i] = true;
                    IsParsedArr[i+1] = true;
                    i++;
                }
            }

            foreach(bool hasUsed in IsParsedArr)
            {
                if (hasUsed == false)
                {
                    hasUnused = true;
                    break;
                }
            }
            if (hasUnused = false)
            {
                return returnOption;
            }
            else
            {
                return new Option
                        {
                            isInteractiveMode = false,
                            inputFile = "",
                            outputFile = "",
                            parsingError = "too much args",
                            splitString = ""
                        };
            }
        
        }
    }
}
