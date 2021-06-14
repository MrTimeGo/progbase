using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ArgumentParser.Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
