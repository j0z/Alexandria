using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace AlexandriaServer
{
    class FileList
    {
        public string[] name;
        public long[] size;

        string downloadLocation = Server.fileLocation;
        

        public void createFileList()
        {
            FileInfo fileInfo;
            name = new string[System.IO.Directory.GetFiles(downloadLocation).GetLength(0)];
            size = new long[System.IO.Directory.GetFiles(downloadLocation).GetLength(0)];
            int n =0;
            foreach (string i in System.IO.Directory.GetFiles(downloadLocation))
            {
                fileInfo = new FileInfo(i);
                size[n] = fileInfo.Length;
                name[n] = i;
                n++;
            }

            json();
        }

        public void loadFileList()
        {
            StreamReader sr = new StreamReader("fileList.txt");
            string input = sr.ReadToEnd();
            FileList list = JsonConvert.DeserializeObject<FileList>(input);

            this.name = list.name;
            this.size = list.size;
        }
    
        private void json()
        {
            string output = JsonConvert.SerializeObject(this);
            StreamWriter fileListFile = new StreamWriter("fileList.txt", false);
            fileListFile.WriteLine(output);
            fileListFile.Flush();
        }

        public string list()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
