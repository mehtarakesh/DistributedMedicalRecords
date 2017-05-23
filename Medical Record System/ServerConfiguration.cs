using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medical_Record_System
{
    public partial class ServerConfiguration : Form
    {
        public ClusterInfo Cluster { get; set; }

        public ServerConfiguration()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text.ToString().Trim();
            string password = textBox3.Text.ToString().Trim();
            string server = ComboBox1.Text.ToString().Trim();
            string keyspace = "demo";
            this.Cluster = null;

            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(server) && !String.IsNullOrEmpty(keyspace))
            {
                this.Cluster = new ClusterInfo(username, password, server, keyspace);
                this.DialogResult = DialogResult.OK;
            }
            
        }
    }
}
