using System;
using System.IO;

namespace lab3
{
    class CsvFileLogger : ILogger
    {
        private StreamWriter sw;
        public CsvFileLogger(string fileString)
        {
            this.sw = new StreamWriter(fileString, true);
        }
        public void Log(string message)
        {
            sw.WriteLine(DateTime.Now.ToString("o") + "," + "LOG" + "," + message);
            sw.Flush();
        }

        public void LogError(string errorMessage)
        {
            sw.WriteLine(DateTime.Now.ToString("o") + "," + "ERROR" + "," + errorMessage);
            sw.Flush();
        }
        public void Close()
        {
            sw.Close();
        }
    }
}
