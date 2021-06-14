using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
namespace Task3
{
    class Repository
    {
        SqliteConnection connection;
        public Repository(string path)
        {
            connection = new SqliteConnection($"Data source = {path}");
        }
        public string GetWinnerByYearAndNomination(int year, string award)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM records WHERE year = $year AND award = $award AND winner = 1";
            command.Parameters.AddWithValue("$year", year);
            command.Parameters.AddWithValue("$award", award);

            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string name = reader.GetString(4);
                connection.Close();
                return name;
            }
            connection.Close();
            return "";
        }
        public List<Record> GetNominationsByFilm(string film)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM records WHERE film = $film";
            command.Parameters.AddWithValue("$film", film);

            SqliteDataReader reader = command.ExecuteReader();
            List<Record> list = new List<Record>();
            while (reader.Read())
            {
                Record ceremony = new Record
                {
                    year = reader.GetInt32(0),
                    ceremony = reader.GetInt32(1),
                    award = reader.GetString(2),
                    winner = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    name = reader.GetString(4),
                    film = reader.IsDBNull(5) ? "" : reader.GetString(5)
                };
                list.Add(ceremony);
            }
            return list;
        }
    }
}
