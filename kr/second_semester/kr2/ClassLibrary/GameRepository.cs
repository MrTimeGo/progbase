using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class GameRepository
    {
        SqliteConnection connection;
        public GameRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }
        public int InsertGame(Game game)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO games (name, genre, publisher)
            VALUES ($name, $genre, $publisher); 
            SELECT last_insert_rowid()";
            command.Parameters.AddWithValue("$name", game.name);
            command.Parameters.AddWithValue("$genre", game.genre);
            command.Parameters.AddWithValue("$publisher", game.publisher);

            long id = (long)command.ExecuteScalar();
            connection.Close();
            return (int)id;
        }
        public Game GetByName(string gameName)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM games WHERE name = $name";
            command.Parameters.AddWithValue("$name", gameName);

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Game game = new Game
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    genre = reader.GetString(2),
                    publisher = reader.GetString(3)
                };
                reader.Close();
                connection.Close();
                return game;
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
        public List<Game> GetGamesByPlatform(int platformId)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT game.id, game.name, game.genre, game.publisher FROM records, platforms WHERE platform_id = $platform_id AND game_id = games.id";
            command.Parameters.AddWithValue("$platform_id", platformId);

            SqliteDataReader reader = command.ExecuteReader();
            List<Game> platforms = new List<Game>();
            while (reader.Read())
            {
                Game platform = new Game
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    genre = reader.GetString(2),
                    publisher = reader.GetString(3)
                };

                platforms.Add(platform);
            }
            reader.Close();
            connection.Close();
            return platforms;
        }
        public List<Game> GetAllPlatforms()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM games";

            SqliteDataReader reader = command.ExecuteReader();
            List<Game> platforms = new List<Game>();
            while (reader.Read())
            {
                Game platform = new Game
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    genre = reader.GetString(2),
                    publisher = reader.GetString(3)
                };

                platforms.Add(platform);
            }
            reader.Close();
            connection.Close();
            return platforms;
        }
    }
}
