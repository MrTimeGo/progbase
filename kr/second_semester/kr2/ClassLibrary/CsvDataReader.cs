using System;
using System.IO;

namespace ClassLibrary
{
    class CsvDataReader
    {
        private StreamReader streamReader;
        private string[] currentRow;

        public CsvDataReader(string csvFilePath)
        {
            streamReader = new StreamReader(csvFilePath);
        }
        public bool Read()
        {
            string row = streamReader.ReadLine();
            if (row == null)
            {
                return false;
            }
            currentRow = row.Split(",");
            return currentRow.Length != 0;
        }
        public string GetString(int index)
        {
            return this.currentRow[index];
        }
        public int GetInt32(int index)
        { 
            if (int.TryParse(GetString(index), out int number))
            {
                return number;
            }
            return -1;
        }
        public void Close()
        {
            streamReader.Close();
        }

    }
}
