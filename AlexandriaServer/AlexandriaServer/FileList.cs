using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlexandriaServer
{
    class FileList
    {
        string[] name;
        long[] size;

        string downloadLocation = Server.fileLocation;

        public void createFileList()
        {
            name = new string[System.IO.Directory.GetFiles(downloadLocation).GetLength(0)];
            int n =0;
            foreach (string i in System.IO.Directory.GetFiles(downloadLocation))
            {
                name[n] = i;
                n++;
            }

            n = 0;

            //foreach(string i in System.IO.File.GetAttributes(downloadLocation).)
        }

        public void json()
        {
        }

    }
}
