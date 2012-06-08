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

        //IPAddress localIP = IPAddress.Broadcast;
        //static readonly IPAddress Any;
        TcpListener tcpListener = new TcpListener(8080);
        //File file;
        char[] seperators = { ':' };

        string fileLocation = "\\files";

        public void runServer()
        {
            Console.WriteLine("Starting server...");
            tcpListener.Start();
            Console.WriteLine("Server Started!");
            //Byte[] bytes = new Byte[16];
            //Byte[] data = new Byte[16];

            bool fileComplete = false;

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Client Connected!");
                NetworkStream stream = tcpClient.GetStream();
                byte[] incomingData = new byte[tcpClient.ReceiveBufferSize];
                int bytesRead = stream.Read(incomingData, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

                string command = Encoding.ASCII.GetString(incomingData, 0, bytesRead);

                Console.WriteLine(command);

                string[] commands = command.Split(seperators);

                foreach (string x in commands)
                {
                    Console.WriteLine(x);
                }

                if (commands[0].Contains("GET"))
                {
                    Console.WriteLine("Getting File! \n" + commands[1]+"test");
                    //file = new File(commands[1], fileLocation);
                    FileStream fs = new FileStream(commands[1], FileMode.Open);

                    int currentPos = 0;

                    Console.WriteLine(fs.Length);

                    while (fileComplete==false)
                    {
                        //Console.WriteLine();
                        byte[] bytes = new byte[fs.Length];
                        int data = fs.Read(bytes, 0, (int)fs.Length);

                        Console.WriteLine(Convert.ToString(bytes));
                        stream.Write(bytes, 0, (int)fs.Length);
                        //currentPos += 16;
                        //if (currentPos >= fs.Length)
                        //{
                            Console.WriteLine("File Complete!");
                            fileComplete = true;
                        //}

                    }

                    if (fileComplete)
                    {
                        Console.WriteLine("File Complete!");

                        stream.Close();
                    }
                }
            }
        }

        public void sendFile(string fileName)
        {
        }
    }
}
