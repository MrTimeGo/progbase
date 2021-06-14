using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using static System.Console;

namespace Task2
{
    static class ConsoleInterface
    {
        public static void Run()
        {
            bool exit = false;
            while (!exit)
            {
                string input = GetString();
                if (input == "exit" || input == "")
                {
                    exit = true;
                    continue;
                }

                string[] subcommands = input.Split(" ");
                try
                {
                    ValidateCommandLength(subcommands.Length);

                    switch (subcommands[0])
                    {
                        case "gen_db": ProcessGenDb(subcommands); break;
                        case "page": ProcessGetAward(subcommands); break;
                        case "merge_csv": ProcessMergeCsv(subcommands); break;
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static void ProcessGenDb(string[] subcommands)
        {
            if (subcommands.Length != 5)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];
            string d = subcommands[2];
            int n = GetNum(subcommands[3]);
            int m = GetNum(subcommands[4]);

            if (!Directory.Exists(d))
            {
                Directory.CreateDirectory(d);
            }

            List<Record> records = DataIO.ReadFromXml(f);
            int count = DataProcessor.CountCeremonies(records);
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                SqliteConnection connection = new SqliteConnection($"Data source = {d + GetRandomName() + ".db"}");
                Repository repo = new Repository(connection);
                repo.CreateTable();

                int indent = rnd.Next(1, count + 1);
                for (int j = 0; j < m; j++)
                {
                    List<Record> ceremonyRecord = DataProcessor.GetRecordByCeremony((indent + j) % (count + 1), records);
                    foreach (Record record in ceremonyRecord)
                    {
                        repo.Insert(record);
                    }
                }
            }
        }
        private static string GetRandomName()
        {
            string pattern = "1234567890";
            Random rnd = new Random();
            string name = "";
            for (int i = 0; i < 8; i++)
            {
                name += pattern[rnd.Next(0, pattern.Length)];
            }
            return name;
        }
        private static void ProcessGetAward(string[] subcommands)
        {
            if (subcommands.Length != 4)
            {
                throw new ArgumentException("Wrong command length");
            }
            string f = subcommands[1];
            int p = GetNum(subcommands[2]);
            int l = GetNum(subcommands[3]);

            SqliteConnection connection = new SqliteConnection($"Data source = {f}");
            Repository repo = new Repository(connection);
            int totalPages = repo.GetTotalPages(l);
            if (p > totalPages || p < 1)
            {
                WriteLine("Wrong page");
                return;
            }
            List<Record> list = repo.GetPage(p, l);
            WriteLine($"Page: {p}/{totalPages}");
            foreach (Record record in list)
            {
                WriteLine(record);
            }
        }
        private static void ProcessMergeCsv(string[] subcommands)
        {
            if (subcommands.Length != 3)
            {
                throw new ArgumentException("Wrong command length");
            }
            string d = subcommands[1];
            string fout = subcommands[2];
            HashSet<Record> set = new HashSet<Record>();

            string[] files = Directory.GetFiles(d);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].EndsWith(".db"))
                {
                    SqliteConnection connection = new SqliteConnection($"Data source = {files[i]}");
                    Repository repo = new Repository(connection);
                    List<Record> records = repo.GetAll();
                    foreach (Record record in records)
                    {
                        set.Add(record);
                    }
                }
            }
            List<Record> list = new List<Record>(set);
            DataIO.WriteToCsv(fout, list);
        }
        private static void ValidateCommandLength(int length)
        {
            if (length < 3)
            {
                throw new ArgumentException("Wrong command legth");
            }
        }
        private static int GetNum(string str)
        {
            if (!int.TryParse(str, out int num))
            {
                throw new ArgumentException("Not integer");
            }
            return num;
        }
        private static string GetString()
        {
            Write("> ");
            return ReadLine();
        }
    }
}
