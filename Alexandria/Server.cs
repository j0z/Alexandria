using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Alexandria
{
    class Server
    {
        
        //IPAddress localIP = IPAddress.Broadcast;
        static readonly IPAddress Any;
        TcpListener tcpListener = new TcpListener(Any, 8080);
        File file;
        char[] seperators = {':'};

        string fileLocation = "\\files";

        public void runServer()
        {
            tcpListener.Start();

            //Byte[] bytes = new Byte[16];
            //Byte[] data = new Byte[16];

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                string command = new StreamReader(stream).ReadToEnd();

                string[] commands = command.Split(seperators);

                if (commands[0].Contains("GET"))
                {
                    file = new File(commands[1], fileLocation);
                    FileStream fs = new FileStream(fileLocation, FileMode.Open);

                    int currentPos = 0;

                    while (currentPos < fs.Length)
                    {
                        byte[] bytes = new byte[16];
                        int data = fs.Read(bytes, currentPos, 16);

                        stream.Write(bytes, 0, 16);
                    }
                }
            }
        }

        public void sendFile(string fileName)
        {
        }
    }
}
