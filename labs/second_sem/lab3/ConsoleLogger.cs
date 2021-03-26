using System;

namespace lab3
{
    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string errorMessage)
        {
            Console.Error.WriteLine(errorMessage);
        }
    }
}
