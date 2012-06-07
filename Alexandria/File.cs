using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria
{
    class File
    {
        private string Name;
        private long totalfileSize;
        private long currentfileSize;
        private string downloadLocation;
        private bool finished;

        public File(string name, string downloadLocation)
        {
            this.Name = name;
            this.totalfileSize = getSize();
            this.currentfileSize = 0L;
            this.downloadLocation = downloadLocation;
            this.finished = false;
        }

        public List<string> getInfo()
        {
            List<string> info = new List<string>();
            info.Add(Name);
            info.Add(totalfileSize.ToString());
            info.Add(currentfileSize.ToString());
            info.Add(downloadLocation);

            return info;
        }

        public void Write(byte[] data)
        {
            currentfileSize += data.Length;

            FileStream fs = new FileStream(Name, FileMode.Append);
            BinaryWriter binaryWriter = new BinaryWriter(fs);
            binaryWriter.Write(data);
            binaryWriter.Close();
            fs.Close();
        }

        public bool isFinished()
        {
            if (currentfileSize >= totalfileSize)
                return true;
            else
                return false;
        }

        long getSize()
        {
            FileInfo file = new FileInfo(Name);
            return file.Length;
        }
    }
}
