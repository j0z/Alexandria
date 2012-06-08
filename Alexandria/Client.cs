using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Alexandria
{
    class Client
    {
        TcpClient tcpClient = new TcpClient(AlexandriaMain.currentServer, 8080);

        

        public void fileGet(string fileName)
        {
            NetworkStream ns = tcpClient.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            File file = new File("test.png", "");

            sw.Write("GET:" + fileName);
            sw.Flush();

            byte[] lengthBytes = new byte[16];

            int read = ns.Read(lengthBytes, 0, 15);
            int length = BitConverter.ToInt32(lengthBytes, 0);
            byte[] buf = new byte[length];
            ns.Read(buf, 0, buf.Length);

            file.Write(buf);
            sw.Close();
            ns.Close();
        }
    }
}
