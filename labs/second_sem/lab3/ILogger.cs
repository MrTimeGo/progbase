using System;

namespace lab3
{
    interface ILogger
    {
        void Log(string message);
        void LogError(string errorMessage);
    }
}
