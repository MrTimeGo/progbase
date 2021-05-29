using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class RecordRepository
    {
        SqliteConnection connection;
        public RecordRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }
        public int InsertRecord(Record record)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO records (game_id, platform_id, year)
            VALUES ($game_id, $platform_id, $year); 
            SELECT last_insert_rowid()";
            command.Parameters.AddWithValue("$game_id", record.gameId);
            command.Parameters.AddWithValue("$platform_id", record.platformId);
            command.Parameters.AddWithValue("$publish_year", record.year);

            long id = (long)command.ExecuteScalar();
            connection.Close();
            return (int)id;
        }
        public int DropTable()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM records";
            int nChanged = command.ExecuteNonQuery();
            connection.Close();
            return nChanged;
        }
        public Record GetByIds(int gameId, int platformId)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM records WHERE platform_id = $platform_id AND game_id = $game_id";
            command.Parameters.AddWithValue("$platform_id", platformId);
            command.Parameters.AddWithValue("$game_id", gameId);

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Record record = new Record
                {
                    id = reader.GetInt32(0),
                    gameId = reader.GetInt32(1),
                    platformId = reader.GetInt32(2),
                    year = reader.GetInt32(3),
                };
                reader.Close();
                connection.Close();
                return record;
            }
            reader.Close();
            connection.Close();
            return null;
        }
        public List<Record> GetAllPlatforms()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM games";

            SqliteDataReader reader = command.ExecuteReader();
            List<Record> platforms = new List<Record>();
            while (reader.Read())
            {
                Record record = new Record
                {
                    id = reader.GetInt32(0),
                    gameId = reader.GetInt32(1),
                    platformId = reader.GetInt32(2),
                    year = reader.GetInt32(3),
                };

                platforms.Add(record);
            }
            reader.Close();
            connection.Close();
            return platforms;
        }
    }
}
