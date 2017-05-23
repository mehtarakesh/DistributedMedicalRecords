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
    public partial class GenerateUploadRecord : Form
    {
        public int NumberOfRecords { get; set; }
        public string ServerName { get; set; }

        public GenerateUploadRecord()
        {
            InitializeComponent();
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumberOfRecords = 0;
            ServerName = "";
            if(!String.IsNullOrEmpty(textBox1.Text.ToString().Trim()) && IsDigitsOnly(textBox1.Text.ToString().Trim()))
                NumberOfRecords = Int32.Parse(textBox1.Text.ToString().Trim());
            if (!String.IsNullOrEmpty(comboBox1.Text.ToString().Trim()))
                ServerName = comboBox1.Text.ToString().Trim();
            if(NumberOfRecords != 0 && !String.IsNullOrEmpty(ServerName))
                this.DialogResult = DialogResult.OK;
        }
    }
}
