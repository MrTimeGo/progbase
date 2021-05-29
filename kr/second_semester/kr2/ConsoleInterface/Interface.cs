using System;
using System.Collections.Generic;
using static System.Console;
using Microsoft.Data.Sqlite;
using ClassLibrary;

namespace ConsoleInterface
{
    static class Interface
    {
        public static void Run(SqliteConnection connection)
        {
            bool exit = false;
            while (!exit)
            {
                string input = ReadCommand();
                if (input == "help")
                {
                    WriteLine(GetHelp());
                    continue;
                }
                string[] subcommands = input.Split(" ");
                try
                {
                    if (subcommands.Length < 2)
                    {
                        throw new ArgumentException("Wrong command length");
                    }

                    if (subcommands[0] == "import")
                    {
                        ProcessImport(subcommands, connection);
                    }
                    else if (subcommands[0] == "export")
                    {
                        ProcessExport(subcommands, connection);
                    }
                    else if (subcommands[0] == "drop")
                    {
                        ProcessDrop(subcommands, connection);
                    }
                    else if (subcommands[0] == "write")
                    {
                        throw new NotImplementedException("Not implemented");
                    }
                    else
                    {
                        throw new ArgumentException("Unknown command");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Error: {ex.Message}");
                }
            }
        }
        static void ProcessDrop(string[] subcommands, SqliteConnection connection)
        {
            if (subcommands[1] == "games")
            {
                GameRepository repo = new GameRepository(connection);
                repo.DropTable();
            }
            else if (subcommands[1] == "platforms")
            {
                PlatformRepository repo = new PlatformRepository(connection);
                repo.DropTable();
            }
            else if (subcommands[1] == "records")
            {
                RecordRepository repo = new RecordRepository(connection);
                repo.DropTable();
            }
            else
            {
                throw new ArgumentException("Unknown command");
            }
        }
        static void ProcessExport(string[] subcommands, SqliteConnection connection)
        {
            PlatformRepository pRepo = new PlatformRepository(connection);
            GameRepository gRepo = new GameRepository(connection);
            if (subcommands[1] == "platforms")
            {
                if (subcommands.Length != 3)
                {
                    throw new ArgumentException("Unknown command");
                }
                XmlExporter.ExportPlatforms(subcommands[2], pRepo);
            }
            else if (subcommands[1] == "game")
            {
                if (subcommands.Length != 4)
                {
                    throw new ArgumentException("Unknown command");
                }
                XmlExporter.ExportGame(subcommands[3], subcommands[2], gRepo, pRepo);
            }
            else if (subcommands[1] == "platform")
            {
                if (subcommands.Length != 4)
                {
                    throw new ArgumentException("Unknown command");
                }
                XmlExporter.ExportPlatform(subcommands[3], subcommands[2], gRepo, pRepo);
            }
            else
            {
                throw new ArgumentException("Unknown command");
            }
        }
        static void ProcessImport(string[] subcommnads, SqliteConnection connection)
        {
            string csvFilePath = subcommnads[1];
            GameRepository gameRepository = new GameRepository(connection);
            PlatformRepository platformRepository = new PlatformRepository(connection);
            RecordRepository recordRepository = new RecordRepository(connection);

            CsvImporter.ImportFromCsv(csvFilePath, gameRepository, platformRepository, recordRepository);
        }
        static string GetHelp()
        {
            return "import {csv file path}\nexport platforms {xml file path}\nexport game {game name} {xml file path}\nexport platform {platform name} {xml file path}\ndrop {games/platforms/records}\nwrite {games/platforms/records}";
        }
        static string ReadCommand()
        {
            Write("> ");
            return ReadLine();
        }
    }
}
