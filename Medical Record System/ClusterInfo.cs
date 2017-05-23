using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_System
{
    public class ClusterInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string server { get; set; }
        public string keyspace { get; set; }

        public ClusterInfo(string username, string pass, string server, string keyspace)
        {
            this.username = username;
            this.password = pass;
            this.server = server;
            this.keyspace = keyspace;
        }

        public void Print()
        {
            Console.WriteLine(username + "," + password + "," + server + "," + keyspace);
        }
    }
}
