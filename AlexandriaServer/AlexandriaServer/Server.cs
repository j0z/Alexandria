﻿using System;
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
        public static string fileLocation = System.Environment.CurrentDirectory + @"\files\";
        public static FileList fileList = new FileList();
        TcpListener tcpListener = new TcpListener(8080);
        serverCommand server = new serverCommand();
        NetworkStream stream;

        char[] seperators = { ':' };


        public void runServer()
        {
            StreamReader fileListReader;

            Console.WriteLine("Alexandria-Server 0.02a \n Written by: \n j0z");
            Console.WriteLine("Starting server...");
            tcpListener.Start();
            Console.WriteLine("Loading filelist...");

            if (File.Exists("fileList.txt"))
            {
                fileList.loadFileList();
                Console.WriteLine("Loaded filelist!");
            }
            else
            {
                Console.WriteLine("Filelist not found, creating...");
                fileList.createFileList();
                Console.WriteLine("Filenames: \n" + fileList.name[0]);
                Console.WriteLine("File sizes: \n" + fileList.size[0]);
                Console.WriteLine("Created filelist!");
            }

            Console.WriteLine("Server Started!");

            bool fileComplete = false;

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Client Connected!");
                stream = tcpClient.GetStream();
                byte[] incomingData = new byte[tcpClient.ReceiveBufferSize];
                int bytesRead = stream.Read(incomingData, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

                string command = Encoding.ASCII.GetString(incomingData, 0, bytesRead);

                Console.WriteLine("Received command: " + command);

                string[] commands = command.Split(seperators);

                foreach (string x in commands)
                {
                    Console.WriteLine(x);
                }

                switch (commands[0])
                {
                    case "get":
                        get(commands[1], commands[2]);
                        break;
                    case "put":
                        put(commands[1], commands[2]);
                        break;
                    default:
                        Console.WriteLine("Invalid command received");
                        break;
                }
            }
        }

        public void get(string variable, string data)
        {
            switch (variable)
            {
                case "inf":
                    server.Ping(stream);
                    break;
                case "fil":
                    server.sendFile(fileLocation+data, stream);
                    break;
                case "fli":
                    Console.WriteLine("Sending filelist");
                    server.List(stream);
                    break;
            }
        }

        public void put(string variable, string data)
        {

        }
    }
}
