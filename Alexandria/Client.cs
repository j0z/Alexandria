using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Alexandria
{
    class Client
    {
        TcpClient tcpClient = new TcpClient(AlexandriaMain.currentServer, 8080);
        public Command command = new Command();
        public NetworkStream networkStream;


        public void Start()
        {
            networkStream = tcpClient.GetStream();
        }
        



    }
}
