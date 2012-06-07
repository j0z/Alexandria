using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria
{
    class Config
    {
         private string _nodeName;
        private string _downloadLocation;

        public string nodeName
        {
            get
            {
                return _nodeName;
            }
            set
            {
                _nodeName = value;
            }
        }
        public string downloadLocation 
        {
            get
            {
                return _downloadLocation;
            }
            set
            {
                _downloadLocation = value;
            }
        }

        public Config()
        {
            _nodeName = "default";
            _downloadLocation = "downloads";
        }

    }
}
