using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.IO;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace kr1
{
    class Show
    {
        public string id;
        public string title;
        public string director;
        public string cast;

        public Show() { }
        public Show(string id, string name, string director, string cast)
        {
            this.id = id;
            this.title = name;
            this.director = director;
            this.cast = cast;
        }
        public override string ToString()
        {
            return $"{id}. {title} by {director}. Cast: {cast}.";
        }
    }
    class List<T>
    {
        private T[] _items;
        private int _size;
        public List()
        {
            this._items = new T[16];
            this._size = 0;
        }
        public void Add(T newData)
        {
            if (_size == _items.Length)
            {
                EnsureCapasity();
            }
            this._items[this._size] = newData;
            this._size += 1;
        }
        public void Insert(int index, T newData)
        {
            if (_size == _items.Length)
            {
                EnsureCapasity();
            }
            for (int i = _size - 1; i >= index; i--)
            {
                _items[i + 1] = _items[i];
            }
            _items[index] = newData;
            _size += 1;
        }
        public bool Remove(T newData)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(newData))
                {
                    for (int j = i; j < _size - 1; j++)
                    {
                        _items[j] = _items[j + 1];
                    }
                    _size -= 1;
                    _items[_size] = default;
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            if (index >= _size)
            {
                throw new IndexOutOfRangeException();
            }
            if (index != _size - 1)
            {
                for (int i = index; i < _size - 1; i++)
                {
                    _items[i] = _items[i + 1];
                }
            }
            _size -= 1;
            _items[_size] = default;
        }
        public void Clear()
        {
            int capacity = _items.Length;
            _items = new T[capacity];
            _size = 0;
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
        }
        public T this[int index]
        {
            get
            {
                if (index >= _size)
                {
                    throw new IndexOutOfRangeException();
                }
                return _items[index];
            }
            set
            {
                if (index >= _size)
                {
                    throw new IndexOutOfRangeException();
                }
                _items[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.Take(this._size).GetEnumerator();
        }

        private void EnsureCapasity()
        {
            int oldCapacity = _items.Length;
            T[] oldArray = _items;
            _items = new T[oldCapacity * 2];
            Array.Copy(oldArray, _items, oldCapacity);
        }
    }
    class Program
    {
        static void GenerateNumbersFile(string f, int a, int b, int n)
        {
            StreamWriter sw = new StreamWriter(f);
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(rand.Next(a, b + 1));
            }
            sw.Close();
        }
        static List<int> ReadSpecificNumberFromFile(string f)
        {
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader(f);
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (int.TryParse(line, out int n) && n % 2 == 0)
                    list.Add(n);
            }
            return list;
        }
        static void ProcessFile(string f)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<int> list = ReadSpecificNumberFromFile(f);
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < list[i -1])
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            watch.Stop();
            WriteLine($"Function ProcessFile() ended its work in {watch.Elapsed}");
        }
        static void DoTask1()
        {
            const string filePath1 = "hund.txt";
            const string filePath2 = "thous.txt";
            const string filePath3 = "tenthous.txt";
            GenerateNumbersFile(filePath1, -100, 100, 100);
            GenerateNumbersFile(filePath2, -100, 100, 1000);
            GenerateNumbersFile(filePath3, -100, 100, 10000);
            ProcessFile(filePath1);
            ProcessFile(filePath2);
            ProcessFile(filePath3);
        }
        static long CountShows(SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM shows";
            long count = (long)command.ExecuteScalar();
            return count;
        }
        static List<Show> ReadSpecificShows(SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM shows WHERE \"cast\" LIKE '%' || $value || '%' ";
            command.Parameters.AddWithValue("$value", "Samuel L. Jackson");

            SqliteDataReader reader = command.ExecuteReader();
            List<Show> list = new List<Show>();
            WriteLine("Specific shows:");
            while(reader.Read())
            {
                Show show = new Show();
                if (reader.IsDBNull(0) || reader.IsDBNull(2) || reader.IsDBNull(3) || reader.IsDBNull(4))
                {
                    continue;
                }
                show.id = reader.GetString(0);
                show.title = reader.GetString(2);
                show.director = reader.GetString(3);
                show.cast = reader.GetString(4);

                WriteLine(show);
                list.Add(show);
            }
            return list;
        }
        static void WriteShow(SqliteConnection connection, Show show)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO shows(id, title, director, cast) VALUES($id, $title, $director, $cast)";
            command.Parameters.AddWithValue("$id", show.id);
            command.Parameters.AddWithValue("$title", show.title);
            command.Parameters.AddWithValue("$director", show.director);
            command.Parameters.AddWithValue("$cast", show.cast);
            int nChanged = command.ExecuteNonQuery();
        }
        static void DoTask2()
        {
            const string db1Path = "database.db";
            const string db2Path = "database2.db";

            SqliteConnection connection1 = new SqliteConnection($"Data Source={db1Path}");
            connection1.Open();
            long numberOfRecords1 = CountShows(connection1);
            WriteLine($"Numeber of shows in the first data base: {numberOfRecords1}");
            List<Show> list = ReadSpecificShows(connection1);
            connection1.Close();


            SqliteConnection connection2 = new SqliteConnection($"Data Source={db2Path}");
            connection2.Open();
            foreach (Show show in list)
            {
                WriteShow(connection2, show);
            }
            long numberOfRecords2 = CountShows(connection2);
            WriteLine($"Numeber of shows in the second data base: {numberOfRecords2}");
            connection2.Close();
        }
        static void Main(string[] args)
        {
            WriteLine("Choose task:");
            string input = ReadLine();
            if (input == "1")
            {
                DoTask1();
            }
            else if (input == "2")
            {
                DoTask2();
            }
            else
            {
                WriteLine("Unknown task");
            }
        }
    }
}
