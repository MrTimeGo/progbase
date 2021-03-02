using System;
using static System.Console;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace SQLpracic
{
    class StringBuilder
    {
        private string[] strings;
        private int size;

        public StringBuilder()
        {
            strings = new string[16];
            size = 0;
        }
        public StringBuilder Append(string str)
        {
            if (strings.Length == size)
            {
                Expand();
            }
            strings[size] = str;
            size += 1;
            return this;
        }
        private void Expand()
        {
            int oldCapacity = strings.Length;
            string[] oldArray = strings;
            strings = new string[oldCapacity * 2];
            Array.Copy(oldArray, strings, oldCapacity);
        }
        private int GetTotalLength()
        {
            int charCounter = 0;
            for (int i = 0; i < size; i++)
            {
                string str = strings[i];
                charCounter += str.Length;
            }
            return charCounter;
        }
        public override string ToString()
        {
            int charCounter = GetTotalLength();
            char[] buffer = new char[charCounter];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                string str = strings[i];
                Array.Copy(str.ToCharArray(), 0, buffer, index, str.Length);
                index += str.Length;
            }
            string allStrings = new string(buffer);
            return allStrings;
        }
    }
    class Food
    {
        public string foodName;
        public string scientificName;
        public string group;
        public string subGroup;
        public override string ToString()
        {
            return $"{foodName} [{scientificName}] - {group}: {subGroup}";
        }
    }
    class ListFood
    {
        private Food[] _items;
        private int _size;
        public ListFood()
        {
            this._items = new Food[16];
            this._size = 0;
        }
        public void Add(Food newCapital)
        {
            if (_size == _items.Length)
            {
                EnsureCapasity();
            }
            this._items[this._size] = newCapital;
            this._size += 1;
        }
        public void Insert(int index, Food newCapital)
        {
            if (_size == _items.Length)
            {
                EnsureCapasity();
            }
            for (int i = _size - 1; i >= index; i--)
            {
                _items[i + 1] = _items[i];
            }
            _items[index] = newCapital;
            _size += 1;
        }
        public bool Remove(Food newCapital)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_items[i] == newCapital)
                {
                    for (int j = i; j < _size - 1; j++)
                    {
                        _items[j] = _items[j + 1];
                    }
                    _size -= 1;
                    _items[_size] = null;
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            if (index != _size - 1)
            {
                for (int i = index; i < _size - 1; i++)
                {
                    _items[i] = _items[i + 1];
                }
            }
            _size -= 1;
            _items[_size] = null;
        }
        public void Clear()
        {
            int capacity = _items.Length;
            _items = new Food[capacity];
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
        public Food this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                _items[index] = value;
            }
        }

        public IEnumerator<Food> GetEnumerator()
        {
            return this._items.Take(this._size).GetEnumerator();
        }

        private void EnsureCapasity()
        {
            int oldCapacity = _items.Length;
            Food[] oldArray = _items;
            _items = new Food[oldCapacity * 2];
            Array.Copy(oldArray, _items, oldCapacity);
        }
    }
    class Program
    {
        static ListFood GetAllFood(SqliteDataReader reader)
        {
            ListFood list = new ListFood();
            while(reader.Read())
            {
                Food food = new Food();
                food.foodName = reader.GetString(0);
                food.scientificName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                food.group = reader.GetString(2);
                food.subGroup = reader.GetString(3);
                list.Add(food);
            }
            return list;
        }
        static string GetCsvFromList(ListFood list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Food food in list)
            {
                sb.Append(food.foodName).Append(",").Append(food.scientificName).Append(",").Append(food.group).Append(",").Append(food.subGroup).Append("\n");
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {
            string filename = "fooddb.db";
            SqliteConnection connection = new SqliteConnection($"Data Source={filename}");
            connection.Open();
            bool exit = false;
            while (!exit)
            {
                Write("> ");
                string input = ReadLine();
                if (input == "getAll")
                {
                    SqliteCommand getAll = connection.CreateCommand();
                    getAll.CommandText = @"SELECT * FROM foods";
                    SqliteDataReader reader = getAll.ExecuteReader();
                    ListFood list = GetAllFood(reader);
                    foreach(Food food in list)
                    {
                        WriteLine(food);
                    }
                }
                else if (input.StartsWith("getExport"))
                {
                    string[] command = input.Split(" ");
                    if (command.Length != 2)
                    {
                        WriteLine("Unknown command");
                        continue;
                    }
                    SqliteCommand getExport = connection.CreateCommand();
                    getExport.CommandText = "SELECT * FROM foods WHERE Groupe = $group";
                    getExport.Parameters.AddWithValue("$group", command[1]);
                    SqliteDataReader reader = getExport.ExecuteReader();
                    ListFood list = GetAllFood(reader);
                    foreach(Food food in list)
                    {
                        WriteLine(food);
                    }
                }
                else if (input.StartsWith("export"))
                {
                    string[] command = input.Split(" ");
                    if (command.Length != 2)
                    {
                        WriteLine("Unknown command");
                        continue;
                    }
                    SqliteCommand export = connection.CreateCommand();
                    export.CommandText = "SELECT * FROM foods WHERE Groupe = $group";
                    export.Parameters.AddWithValue("$group", command[1]);
                    SqliteDataReader reader = export.ExecuteReader();
                    ListFood list = GetAllFood(reader);
                    string csvText = GetCsvFromList(list);
                    System.IO.File.WriteAllText("output.csv", csvText);
                }
                else if (input == "exit")
                {
                    exit = true;
                }
                else
                {
                    WriteLine("Unknown command");
                }
            }
            connection.Close();
        }
    }
}
