using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task3
{
    class Server
    {
        Repository repo;
        public Server(Repository repo)
        {
            this.repo = repo;
        }
        public void Run()
        {
            IPAddress ipAddress = IPAddress.Loopback;
            int port = 3000;

            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                socket.Bind(localEndPoint);
                socket.Listen();
                while (true)
                {
                    Console.WriteLine("Listening...");

                    Socket handler = socket.Accept();
                    try
                    {
                        ProcessClient(handler);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void ProcessClient(Socket handler)
        {
            byte[] bytes = new byte[1024];
            string data = "";
            while (true)
            {
                int nBytes = handler.Receive(bytes);
                data += Encoding.UTF8.GetString(bytes, 0, nBytes);
                if (data.Split(" ").Length > 2)
                {
                    break;
                }
            }
            Console.WriteLine($"Get request:\n{data}...");

            string[] getRequest = data.Split(" ");
            string[] subcommands = getRequest[1].Split("/");

            string xml = RequestProcessor.GetResponse(subcommands, repo);

            string response = $"HTTP/1.1 OK 200\nContent-Type: application/xml\nContent-Length: {xml.Length}\n\n{xml}";
            Console.WriteLine($"Sending response:\n{response}");
            bytes = Encoding.UTF8.GetBytes(response);
            handler.Send(bytes);
        }
    }
}
