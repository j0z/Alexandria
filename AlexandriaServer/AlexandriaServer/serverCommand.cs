using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace AlexandriaServer
{
    class serverCommand
    {

        /// <summary>
        /// Sends the specified file to the designated Networkstream.
        /// Returns TRUE when finished.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool sendFile(string filename, NetworkStream stream)
        {
            StreamWriter sw = new StreamWriter(stream);
            bool fileComplete = false;

            Console.WriteLine("Getting File: \n" + filename);
            FileStream fs = new FileStream(filename, FileMode.Open);

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

            fs.Close();
            return true;
        }

        /// <summary>
        /// Pings the client and awaits a response.
        /// Returns TRUE when the response is received.
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
        /// not implimented yet, will throw a NotImplimentedException
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="security"></param>
        public void handShake(NetworkStream ns, string security)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not implimented yet, will throw a NotImplimentedException
        /// </summary>
        /// <param name="ns"></param>
        public void getInfo(NetworkStream ns)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not implimented yet, will throw a NotImplimentedException
        /// </summary>
        /// <param name="ns"></param>
        public void List(NetworkStream ns)
        {
            throw new NotImplementedException();


        }
            

    }
}
