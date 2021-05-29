using System;
using ClassLibrary;
using Microsoft.Data.Sqlite;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            const string databaseFilePath = @"./../database.sqlite";
            SqliteConnection connection = new SqliteConnection($"Data source = {databaseFilePath}");
                
            Interface.Run(connection);
            
        }
    }
}
