using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using static System.Console;

namespace lab1Part2
{
    class Capital
    {
        private int id;
        private string name;
        private string country;
        private int population;
        private double area;
        public Capital()
        {

        }
        public Capital(int id, string name, string country, int population, double area)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.population = population;
            this.area = area;
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
        public int Population
        {
            get
            {
                return population;
            }
        }
        public string GetCsvString()
        {
            return id + ";" + name + ";" + country + ";" + population + ";" + area;
        }
        public override string ToString()
        {
            return string.Format("ID: {0} - Name: {1} - Country: {2} - Population: {3} - Area: {4}", id, name, country, population, area);
        }
    }
    class ListCapital
    {
        private Capital[] _items;
        private int _size;
        public ListCapital()
        {
            this._items = new Capital[16];
            this._size = 0;
        }
        public void Add(Capital newCapital)
        {
            if (_size == _items.Length)
            {
                EnsureCapasity();
            }
            this._items[this._size] = newCapital;
            this._size += 1;
        }
        public void Insert(int index, Capital newCapital)
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
        public bool Remove(Capital newCapital)
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
            _items = new Capital[capacity];
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
        public Capital this[int index]
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

        public IEnumerator<Capital> GetEnumerator()
        {
            return this._items.Take(this._size).GetEnumerator();
        }

        private void EnsureCapasity()
        {
            int oldCapacity = _items.Length;
            Capital[] oldArray = _items;
            _items = new Capital[oldCapacity * 2];
            Array.Copy(oldArray, _items, oldCapacity);
        }
    }
    class Program
    {
        static ListCapital ReadAllCapitals(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            ListCapital list = new ListCapital();
            string line = "";
            bool isFirstLine = true;
            while (true)
            {
                line = sr.ReadLine();
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }
                if (line == null)
                {
                    break;
                }
                string[] csvData = line.Split(";");
                if (csvData.Length != 5)
                {
                    throw new Exception("CSV number of column is incorrect");
                }
                else if (!int.TryParse(csvData[0], out int id))
                {
                    throw new Exception("First columnt is not integer");
                }
                else if (!int.TryParse(csvData[3], out int population))
                {
                    throw new Exception("Fourth columnt is not integer");
                }
                else if (!double.TryParse(csvData[4], out double area))
                {
                    throw new Exception("Fifth column is not double");
                }
                else
                {
                    list.Add(new Capital(id, csvData[1], csvData[2], population, area));
                }
            }
            sr.Close();
            return list;
        }
        static void WriteFirstTenElements(ListCapital list)
        {
            int length = list.Count < 10 ? list.Count : 10;
            for (int i = 0; i < length; i++)
            {
                WriteLine(list[i].ToString());
            }
        }
        static ListCapital GenerateNewList(ListCapital list1, ListCapital list2)
        {
            ListCapital resultList = new ListCapital();
            int length = list1.Count > list2.Count ? list1.Count : list2.Count;
            bool[] isElementInResultList = new bool[length]; 
            for (int i = 0; i < list1.Count; i++)
            {
                if (!isElementInResultList[i])
                {
                    resultList.Add(list1[i]);
                    isElementInResultList[i] = true;
                }
            }
            for (int i = 0; i < list2.Count; i++)
            {
                if (!isElementInResultList[i])
                {
                    resultList.Add(list2[i]);
                    isElementInResultList[i] = true;
                }
            }
            return resultList;
        }
        static double FindAveragePopulation(ListCapital list)
        {
            long sum = 0;
            foreach(Capital capital in list)
            {
                sum += capital.Population;
            }
            return sum / (double)list.Count;
        }
        static void RemoveItemWithLessThanAveragePopulation(double average, ListCapital list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Population < average)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
        static void WriteAllCapitals(string filePath, ListCapital list)
        {
            StreamWriter sw = new StreamWriter(filePath);
            foreach (Capital capital in list)
            {
                sw.WriteLine(capital.GetCsvString());
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            const string file1Path = @"D:\progbase\labs\second_sem\lab1\lab1Part1\small.csv";
            const string file2Path = @"D:\progbase\labs\second_sem\lab1\lab1Part1\large.csv";
            const string file3Path = @".\output.csv";

            ListCapital list1 = ReadAllCapitals(file1Path);
            WriteLine("Size of first list: {0}", list1.Count);
            WriteFirstTenElements(list1);
            WriteLine("-------------");
            ListCapital list2 = ReadAllCapitals(file2Path);
            WriteLine("Size of second list: {0}", list2.Count);
            WriteFirstTenElements(list2);
            WriteLine("-------------");
            ListCapital list3 = GenerateNewList(list1, list2);
            double averagePopulation = FindAveragePopulation(list3);
            WriteLine("\nAverage: {0}\n", averagePopulation);
            RemoveItemWithLessThanAveragePopulation(averagePopulation, list3);
            WriteLine("-------------");
            WriteAllCapitals(file3Path, list3);
        }
    }
}