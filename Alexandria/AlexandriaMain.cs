using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Alexandria
{
    class AlexandriaMain
    {

        public Config config = new Config();
        public Client client;
        //public Server server;

        public static string currentServer = "localhost";

        public void startUp()
        {
            readConfig();
            client = new Client();
            client.Start();
            //server = new Server();
            //server.runServer();
            
        }

        public void readConfig()
        {
            try
            {
                StreamReader configFile = new StreamReader("config.conf");
                string configString = configFile.ReadLine();
                config = JsonConvert.DeserializeObject<Config>(configString);
            }
            catch (FileNotFoundException)
            {
                StreamWriter configFile = new StreamWriter("config.conf");

                string defaultSettings = JsonConvert.SerializeObject(config);

                configFile.WriteLine(defaultSettings);
                configFile.Close();

                readConfig();
            }
        }

        //Write the new configurations to the file. 
        public void writeConfig()
        {
            string output = JsonConvert.SerializeObject(config);

            StreamWriter configFile = new StreamWriter("config.conf", false);
            configFile.WriteLine(output);
        }
    }
}
