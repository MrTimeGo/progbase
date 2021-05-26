using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class GoodRepository
    {
        private SqliteConnection connection;
        public GoodRepository(string databaseFile)
        {
            this.connection = new SqliteConnection($"Data Source = {databaseFile}");
        }
        public long Insert(Good good)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO goods (name, description, price, is_available, created_at) 
                VALUES ($name, $description, $price, $is_available, $created_at);
 
                SELECT last_insert_rowid();
            ";
            command.Parameters.AddWithValue("$name", good.name);
            command.Parameters.AddWithValue("$description", good.description);
            command.Parameters.AddWithValue("$price", good.price);
            command.Parameters.AddWithValue("$is_available", good.isAvailable);
            command.Parameters.AddWithValue("$created_at", good.createdAt.ToString("o"));

            long newId = (long)command.ExecuteScalar();
            connection.Close();
            return newId;
        }
        public void Delete(long id)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM goods WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteScalar();
            connection.Close();
        }
        public void Edit(Good good)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE goods
                SET name = $name, description = $description, price = $price, is_available = $is_available, created_at = $created_at
                WHERE id = $id
            ";
            command.Parameters.AddWithValue("$id", good.id);
            command.Parameters.AddWithValue("$name", good.name);
            command.Parameters.AddWithValue("$description", good.description);
            command.Parameters.AddWithValue("$price", good.price);
            command.Parameters.AddWithValue("$is_available", good.isAvailable);
            command.Parameters.AddWithValue("$created_at", good.createdAt);

            command.ExecuteNonQuery();
            connection.Close();
        }
        public Good GetById(long id)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM goods WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);

            SqliteDataReader reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                Good good = new Good()
                {
                    id = id,
                    name = reader.GetString(1),
                    description = reader.GetString(2),
                    price = reader.GetDouble(3),
                    isAvailable = reader.GetBoolean(4),
                    createdAt = reader.GetDateTime(5)
                };
                reader.Close();
                connection.Close();
                return good;
            }
            return null;
        }
        public List<Good> GetPage(int pageNumber)
        {
            const int pageSize = 10;
            int numberOfPages = GetTotalPages();
            if (pageNumber > numberOfPages || pageNumber <= 0)
            {
                throw new Exception("Page is not valid");
            }
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM goods LIMIT $limit OFFSET $offset";
            command.Parameters.AddWithValue("$limit", pageSize);
            command.Parameters.AddWithValue("$offset", (pageNumber - 1) * pageSize);

            SqliteDataReader reader = command.ExecuteReader();
            List<Good> page = new List<Good>();
            while (reader.Read())
            {
                Good good = new Good()
                {
                    id = reader.GetInt64(0),
                    name = reader.GetString(1),
                    description = reader.GetString(2),
                    price = reader.GetDouble(3),
                    isAvailable = reader.GetBoolean(4),
                    createdAt = reader.GetDateTime(5)
                };
                page.Add(good);
            }
            reader.Close();
            connection.Close();
            return page;
        }
        public int GetTotalPages()
        {
            const int pageSize = 10;
            return (int)Math.Ceiling(GetCount() / (double)pageSize);
        }
        private long GetCount()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM goods";
            long count = (long)command.ExecuteScalar();
            connection.Close();
            return count;
        }
    }
}
