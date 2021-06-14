using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository("./database.db");
            Server server = new Server(repo);
            server.Run();
        }
    }
}
