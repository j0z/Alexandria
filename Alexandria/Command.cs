using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Alexandria
{
    class Command
    {

        /// <summary>
        /// Pings the server on the specified NetworkStream and returns
        /// true if it receives a reply
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public bool Ping(NetworkStream ns)
        {
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.Write("get:pin:ping");
            sw.Flush();

            string pong = sr.ReadLine();

            if (pong == "put:pin:pong")
                return true;
            else
                return false;
        }

        /// <summary>
        /// First packet required when connecting. The "security" field 
        /// is the security used by the node.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="security"></param>
        public void handShake(NetworkStream ns, string security)
        {
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.Write("get:hnd:" + security);
            sw.Flush();
        }

        /// <summary>
        /// Private networks only- sends the password to the node. 
        /// True means the password was accepted.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Password(NetworkStream ns, string password)
        {
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.Write("put:pwd:" + password);
            sw.Flush();

            string response = sr.ReadLine();

            if (response == "put:pwd:okay")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Final part of the handshake process. 
        /// Server responds with a JSON string.
        /// If there is an error, returns the string "Error".
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public string Info(NetworkStream ns)
        {
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.Write("get:inf:info");
            sw.Flush();

            string response = sr.ReadLine();

            if(response.Contains("put:inf:"))
                return response;
            else
                return "Error";
        }

        /// <summary>
        /// Requests the specified file from the server.
        /// After the download is finished, the client sends "get:fie:end"
        /// to tell the server it is done downloading.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ns"></param>
        public void fileGet(string fileName, NetworkStream ns)
        {
            StreamWriter sw = new StreamWriter(ns);
            File file = new File(fileName, "");

            sw.Write("get:fil:" + fileName);
            sw.Flush();
            byte[] buf = new byte[8100];

            do
            {
                ns.Read(buf, 0, buf.Length);
                file.Write(buf);
            }
            while (ns.DataAvailable);

            sw.Write("get:fie:end");
            sw.Flush();
        }

        /// <summary>
        /// Not implimented yet. Will throw a NotImplementedException().
        /// </summary>
        /// <param name="ns"></param>
        public void Broadcast(NetworkStream ns)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Requests the list of files from the server. 
        /// Server responds with a JSON string containing 32 entries
        /// When all entries have been received, it sends "PUT:FLI:OKAY"
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public string List(NetworkStream ns)
        {
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            int currentFile = 0;
            string fileList = "";

            while (sr.ReadLine() != "put:fli:okay")
            {
                sw.Write("get:fli:" + currentFile.ToString());
                fileList += sr.ReadLine().Remove(0, 9);
            }

            return fileList;
        }
    }
}
