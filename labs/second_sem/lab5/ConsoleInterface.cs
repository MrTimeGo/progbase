using System;
using static System.Console;
using System.Collections.Generic;

namespace lab5
{
    static class ConsoleInterface
    {
        public static void Run()
        {
            bool exit = false;
            DataProcessor dataProcessor = new DataProcessor();
            while (!exit)
            {
                string command = ReadCommand();
                Arguments args;
                try
                {
                    args = ParseCommand(command);
                }
                catch (Exception ex)
                {
                    WriteLine($"Parsing error: {ex.Message}");
                    continue;
                }


                try
                {
                    switch (args.operation)
                    {
                        case "load":
                            {
                                ProcessLoad(args, dataProcessor);
                                break;
                            }
                        case "print":
                            {
                                ProcessPrint(args, dataProcessor);
                                break;
                            }
                        case "save":
                            {
                                ProcessSave(args, dataProcessor);
                                break;
                            }
                        case "export":
                            {
                                ProcessExport(args, dataProcessor);
                                break;
                            }
                        case "subjects":
                            {
                                ProcessSubjects(args, dataProcessor);
                                break;
                            }
                        case "subject":
                            {
                                ProcessSubject(args, dataProcessor);
                                break;
                            }
                        case "instructors":
                            {
                                ProcessInstructors(args, dataProcessor);
                                break;
                            }
                        case "image":
                            {
                                ProcessImage(args, dataProcessor);
                                break;
                            }
                        case "help":
                            {
                                WriteLine(GetHelp());
                                break;
                            }
                        case "exit":
                        case "":
                            {
                                exit = true;
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Processing error: {ex.Message}");
                }
            }
        }
        private static void ProcessImage(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation 'image' should have 1 argument. Got: {args.otherArguments.Length}");
            }
            if (dataProcessor.Courses.Count == 0)
            {
                throw new Exception("Buffer does not contains any cources to generate graphics");
            }

            string[] labels = dataProcessor.GetUniqueSubjects();
            float[] values = dataProcessor.GetSums();

            GraphicsGenerator.CreateGraphics(args.otherArguments[0], labels, values);
            WriteLine($"Image was saved to {args.otherArguments[0]}");
        }
        private static void ProcessSubjects(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 0)
            {
                throw new ArgumentException($"Operation 'subjects' should have 0 argument. Got: {args.otherArguments.Length}");
            }

            string[] subjects = dataProcessor.GetUniqueSubjects();
            if (subjects.Length != 0)
            {
                WriteLine("Subjects:");
                for (int i = 0; i < subjects.Length; i++)
                {
                    WriteLine(subjects[i]);
                }
            }
            else
            {
                WriteLine("There is no subjects");
            }
        }
        private static void ProcessSubject(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation 'subject' should have 0 argument. Got: {args.otherArguments.Length}");
            }

            string[] titles = dataProcessor.GetTitlesBySubject(args.otherArguments[0]);

            if (titles.Length != 0)
            {
                WriteLine("Courses:");
                for (int i = 0; i < titles.Length; i++)
                {
                    WriteLine(titles[i]);
                }
            }
            else
            {
                WriteLine($"There is no courses for this subject: {args.otherArguments[0]}");
            }
        }
        private static void ProcessInstructors(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 0)
            {
                throw new ArgumentException($"Operation 'instructors' should have 0 argument. Got: {args.otherArguments.Length}");
            }

            string[] instructors = dataProcessor.GetUniqueInstructors();
            if (instructors.Length != 0)
            {
                WriteLine("Instructors:");
                for (int i = 0; i < instructors.Length; i++)
                {
                    WriteLine(instructors[i]);
                }
            }
            else
            {
                WriteLine("There is no instructors");
            }
        }
        private static void ProcessExport(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 2)
            {
                throw new ArgumentException($"Operation 'export' should have 2 argument. Got: {args.otherArguments.Length}");
            }
            if (!int.TryParse(args.otherArguments[0], out int n) && n > 0)
            {
                throw new ArgumentException($"Value should be positive integer. Got: {args.otherArguments[0]}");
            }

            List<Course> exportData = dataProcessor.GetExport(n);
            XmlDataIO.StoreCoursesToFile(args.otherArguments[1], exportData);

            WriteLine($"{exportData.Count} courses was exported to {args.otherArguments[1]}");
        }
        private static void ProcessSave(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation 'save' should have 1 argument. Got: {args.otherArguments.Length}");
            }

            XmlDataIO.StoreCoursesToFile(args.otherArguments[0], dataProcessor.Courses);
        }
        private static void ProcessPrint(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation 'print' should have 1 argument. Got: {args.otherArguments.Length}");
            }
            if (!int.TryParse(args.otherArguments[0], out int pageNumber))
            {
                throw new ArgumentException($"Value should be integer. Got: {args.otherArguments[0]}");
            }

            List<Course> page = dataProcessor.GetPage(pageNumber);

            WriteLine($"Page: {pageNumber}/{dataProcessor.GetPageCount()}\n");

            foreach(Course course in page)
            {
                WriteLine(course);
            }
        }
        private static void ProcessLoad(Arguments args, DataProcessor dataProcessor)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation 'load' should have 1 argument. Got: {args.otherArguments.Length}");
            }

            List<Course> courses = XmlDataIO.GetCoursesFromFile(args.otherArguments[0]);
            dataProcessor.Courses = courses;
            WriteLine($"{courses.Count} courses was loaded from {args.operation[0]}");
        }
        private static string GetHelp()
        {
            string[] commands = new string[] { "load {filePath}", "print {pageNum}", "save {filePath}",
                "export {N} {filePath}", "subjects", "subject {subj}", "instructors" , "image {filePath}" };
            string[] descriptions = new string[] { "deserialize data from XML file", "print page of data", "serialize all data XML to XML file",
                "serialize part of data to XML file", "list of all unique subjects", "list of cources of subject", "list of all unique instructors" , "create graphics" };
            string helpString = "";
            for (int i = 0; i < commands.Length; i++)
            {
                helpString += commands[i] + " - " + descriptions[i] + (i == commands.Length - 1 ? "" : "\n");
            }
            return helpString;
        }
        private static Arguments ParseCommand(string command)
        {
            string[] subcommads = command.Trim().Split(" ");
            ValidateCommandLength(subcommads.Length);
            string operation = subcommads[0];
            ValidateOperations(operation);
            string[] otherArguments = new string[subcommads.Length - 1];
            for (int i = 0; i < otherArguments.Length; i++)
            {
                otherArguments[i] = subcommads[i + 1];
            }

            return new Arguments()
            {
                operation = operation,
                otherArguments = otherArguments
            };
        }
        private static void ValidateOperations(string operation)
        {
            string[] validOperations = new string[] { "load", "print", "save", "export", "subjects", "subject", "instructors", "image", "help", "exit", "" };
            for (int i = 0; i < validOperations.Length; i++)
            {
                if (validOperations[i] == operation)
                {
                    return;
                }
            }
            throw new ArgumentException($"Unknown command");
        }
        private static void ValidateCommandLength(int length)
        {
            if (length < 1)
            {
                throw new ArgumentException($"Command length should be more than 0. Got: {length}");
            }
        }
        struct Arguments
        {
            public string operation;
            public string[] otherArguments;
        }
        private static string ReadCommand()
        {
            Write("> ");
            return ReadLine();
        }
    }
}
