using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Task2
{
    class Repository
    {
        SqliteConnection connection;
        public Repository(SqliteConnection connection)
        {
            this.connection = connection;
        }
        public void CreateTable()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE awards 
            (year INTEGER, 
            ceremony INTEGER, 
            award TEXT, 
            winner INTEGER, 
            name TEXT, 
            film TEXT)";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public int Insert(Record record)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT into awards (year, ceremony, award, winner, name, film)
                                                VALUES ($year, $ceremony, $award, $winner, $name, $film);
                                    SELECT last_insert_rowid()";
            command.Parameters.AddWithValue("$year", record.year);
            command.Parameters.AddWithValue("$ceremony", record.ceremony);
            command.Parameters.AddWithValue("$award", record.award);
            command.Parameters.AddWithValue("$winner", record.winner);
            command.Parameters.AddWithValue("$name", record.name);
            command.Parameters.AddWithValue("$film", record.film);

            long newId = (long)command.ExecuteScalar();
            connection.Close();
            return (int)newId;
        }
        public List<Record> GetAll()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM awards";

            SqliteDataReader reader = command.ExecuteReader();

            List<Record> list = new List<Record>();
            while (reader.Read())
            {
                Record record = new Record
                {
                    year = reader.GetInt32(0),
                    ceremony = reader.GetInt32(1),
                    award = reader.GetString(2),
                    winner = reader.GetString(3),
                    name = reader.GetString(4),
                    film = reader.GetString(5)
                };

                list.Add(record);
            }
            connection.Close();

            return list;
        }
        public List<Record> GetPage(int p, int l)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM awards LIMIT $limit OFFSET $offset";
            command.Parameters.AddWithValue("$limit", l);
            command.Parameters.AddWithValue("$offset", (p - 1) * l);

            SqliteDataReader reader = command.ExecuteReader();

            List<Record> records = new List<Record>();
            while (reader.Read())
            {
                Record record = new Record
                {
                    year = reader.GetInt32(0),
                    ceremony = reader.GetInt32(1),
                    award = reader.GetString(2),
                    winner = reader.GetString(3),
                    name = reader.GetString(4),
                    film = reader.GetString(5)
                };
                records.Add(record);
            }
            return records;
        }
        public int GetTotalPages(int l)
        {
            return (int)Math.Ceiling(GetCount() / (double)l);
        }
        public int GetCount()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM awards";

            long count = (long)command.ExecuteScalar();
            connection.Close();
            return (int)count;
        }
    }
}
