using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class PlatformRepository
    {
        SqliteConnection connection;
        public PlatformRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }
        public int InsertPlatform(Platform platform)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO platforms (name)
                                    VALUES ($name); 
                                    SELECT last_insert_rowid()";
            command.Parameters.AddWithValue("$name", platform.name);

            long id = (long)command.ExecuteScalar();
            connection.Close();
            return (int)id;
        }
        public Platform GetByName(string platformName)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM platforms WHERE name = $name";
            command.Parameters.AddWithValue("$name", platformName);

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Platform platform = new Platform
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1)
                };
                reader.Close();
                connection.Close();
                return platform;
            }
            reader.Close();
            connection.Close();
            return null;
        }
        public int DropTable()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM platforms";
            int nChanged = command.ExecuteNonQuery();
            connection.Close();
            return nChanged;
        }
        public List<Platform> GetPlatformsByGame(int gameId)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT platforms.id, platforms.name FROM records, platforms WHERE game_id = $game_id AND platform_id = platforms.id";
            command.Parameters.AddWithValue("$game_id", gameId);

            SqliteDataReader reader = command.ExecuteReader();
            List<Platform> platforms = new List<Platform>();
            while (reader.Read())
            {
                Platform platform = new Platform
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1)
                };

                platforms.Add(platform);
            }
            reader.Close();
            connection.Close();
            return platforms;
        }
        public List<Platform> GetAllPlatforms()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM platforms";

            SqliteDataReader reader = command.ExecuteReader();
            List<Platform> platforms = new List<Platform>();
            while (reader.Read())
            {
                Platform platform = new Platform
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1)
                };

                platforms.Add(platform);
            }
            reader.Close();
            connection.Close();
            return platforms;
        }
    }
}
