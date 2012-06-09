using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace AlexandriaServer
{
    class Server
    {
        TcpListener tcpListener = new TcpListener(8080);

        char[] seperators = { ':' };

        string fileLocation = "\\files";

        public void runServer()
        {
            Console.WriteLine("Alexandria-Server 0.02a \n Written by: \n j0z");
            Console.WriteLine("Starting server...");
            tcpListener.Start();
            Console.WriteLine("Server Started!");

            bool fileComplete = false;

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Client Connected!");
                NetworkStream stream = tcpClient.GetStream();
                byte[] incomingData = new byte[tcpClient.ReceiveBufferSize];
                int bytesRead = stream.Read(incomingData, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

                string command = Encoding.ASCII.GetString(incomingData, 0, bytesRead);

                Console.WriteLine("Received command: " + command);

                string[] commands = command.Split(seperators);

                foreach (string x in commands)
                {
                    Console.WriteLine(x);
                }

                if (commands[0].Contains("get"))
                {
                    if (commands[1].Contains("fil"))
                    {
                        Console.WriteLine("Getting File: \n" + commands[2]);
                        FileStream fs = new FileStream(commands[2], FileMode.Open);

                        Console.WriteLine("File is " + fs.Length + " bytes long \n");

                        while (fileComplete == false)
                        {
                            byte[] bytes = new byte[fs.Length];
                            int data = fs.Read(bytes, 0, (int)fs.Length);

                            Console.WriteLine(Convert.ToString(bytes));
                            stream.Write(bytes, 0, (int)fs.Length);
                            stream.Flush();
                            Console.WriteLine("File Complete!");
                            fileComplete = true;
                        }

                        if (fileComplete)
                        {
                            stream.Close();
                            runServer();
                        }
                    }
                }
            }
        }

        public void sendFile(string fileName)
        {
        }
    }
}
