using Cassandra;
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
    public partial class MainPage : Form
    {
        ClusterInfo[] Clusters;
        bool[] ClusterStatus;

        public MainPage()
        {
            Clusters = new ClusterInfo[3];
            ClusterStatus = new Boolean[3];
            for (int i = 0; i < 3; i++)
            {
                ClusterStatus[i] = false;
            }
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateUploadRecord gur = new GenerateUploadRecord();
            gur.ShowDialog();
            if (gur.NumberOfRecords != 0 && !String.IsNullOrEmpty(gur.ServerName))
            {
                textBox1.AppendText(gur.NumberOfRecords + " records are being generated and uploaded to " + gur.ServerName + ".");
                textBox1.AppendText(Environment.NewLine);
                ClusterInfo cluster;
                if (gur.ServerName.CompareTo("Server1") == 0)
                {
                    cluster = Clusters[0];
                }
                else if (gur.ServerName.CompareTo("Server2") == 0)
                {
                    cluster = Clusters[1];
                }
                else
                {
                    cluster = Clusters[2];
                }
                AutoRecordGeneration gen = new AutoRecordGeneration();
                progressBar.Value = 0;
                progressBar.Maximum = gur.NumberOfRecords;
                progressBar.Step = 1;
                progressBar.Visible = true;
                gen.GenerateRecords(cluster, gur.NumberOfRecords, progressBar);
                textBox1.AppendText("Task is completed.");
                textBox1.AppendText(Environment.NewLine);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QueryRecord qr = new QueryRecord();
            qr.ShowDialog();

            if(qr.NumberOfRecords != 0 && !String.IsNullOrEmpty(qr.ServerName))
            {
                textBox1.AppendText(qr.NumberOfRecords + " records are being fetched from " + qr.ServerName + ".");
                textBox1.AppendText(Environment.NewLine);

                ClusterInfo cluster;
                if (qr.ServerName.CompareTo("Server1") == 0)
                {
                    cluster = Clusters[0];
                }
                else if (qr.ServerName.CompareTo("Server2") == 0)
                {
                    cluster = Clusters[1];
                }
                else
                {
                    cluster = Clusters[2];
                }

                string queryString = "SELECT firstName, lastName, date_of_birth, phone_no, address FROM patient_record LIMIT " + qr.NumberOfRecords;
                Comm comm = new Comm(cluster);
                comm.Connect();
                ISession session = comm.GetSession();

                PreparedStatement ps = session.Prepare(queryString);
                BoundStatement bs = ps.Bind();

                RowSet result = session.Execute(bs);
                int count = 0;
                foreach(Row row in result)
                {
                    textBox1.AppendText(row[0] + ", " + row[1] + ", " + row[2] + ", " + row[3] + ", " + row[4]);
                    textBox1.AppendText(Environment.NewLine);
                    count++;
                }
                textBox1.AppendText(count + " records have been fetched from " + qr.ServerName +".");
                textBox1.AppendText(Environment.NewLine);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateUploadRecord gur = new GenerateUploadRecord();
            gur.ShowDialog();
            if (gur.NumberOfRecords != 0 && !String.IsNullOrEmpty(gur.ServerName))
            {
                ClusterInfo cluster;
                if (gur.ServerName.CompareTo("Server1") == 0)
                {
                    cluster = Clusters[0];
                }
                else if (gur.ServerName.CompareTo("Server2") == 0)
                {
                    cluster = Clusters[1];
                }
                else
                {
                    cluster = Clusters[2];
                }

                textBox1.AppendText("First " + gur.NumberOfRecords + " records are being modified!");
                textBox1.AppendText(Environment.NewLine);

                AutoRecordGeneration gen = new AutoRecordGeneration();
                progressBar.Value = 0;
                progressBar.Maximum = gur.NumberOfRecords;
                progressBar.Step = 1;
                progressBar.Visible = true;
                bool result = gen.ModifyRecord(cluster, gur.NumberOfRecords, progressBar);

                if (result)
                {
                    textBox1.AppendText("First " + gur.NumberOfRecords + " records have been modified successfully!");
                    textBox1.AppendText(Environment.NewLine);
                }                    
                else
                {                    
                    textBox1.AppendText("Modification failed!");
                    textBox1.AppendText(Environment.NewLine);
                }
                    
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Checking consistency of the cluster.");
            textBox1.AppendText(Environment.NewLine);
            bool consistent = RecordComparison.CompareRecords(Clusters[0], Clusters[1], Clusters[2], progressBar);


            if (consistent)
            {
                textBox1.AppendText("The cluster is consistent!");
                textBox1.AppendText(Environment.NewLine);
            }
            else
            {
                textBox1.AppendText("The cluster is NOT consistent!");
                textBox1.AppendText(Environment.NewLine);
            }                
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ServerConfiguration fm1 = new ServerConfiguration();
            if (!ClusterStatus[0])
            {
                fm1.ShowDialog();
                if(fm1.Cluster != null)
                {
                    Clusters[0] = fm1.Cluster;
                    Comm comm = new Comm(Clusters[0]);
                    ClusterStatus[0] = comm.CheckStatus();
                    if (ClusterStatus[0])
                    {
                        this.pictureBox1.Image = global::Medical_Record_System.Properties.Resources.server_online;
                        this.pictureBox1.Invalidate();
                        textBox1.AppendText("Server 1 is Online.");
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
                
            }
                
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ServerConfiguration fm1 = new ServerConfiguration();
            if (!ClusterStatus[1])
            {
                fm1.ShowDialog();
                if (fm1.Cluster != null)
                {
                    Clusters[1] = fm1.Cluster;
                    Comm comm = new Comm(Clusters[1]);
                    ClusterStatus[1] = comm.CheckStatus();
                    if (ClusterStatus[1])
                    {
                        this.pictureBox2.Image = global::Medical_Record_System.Properties.Resources.server_online;
                        this.pictureBox2.Invalidate();
                        textBox1.AppendText("Server 2 is Online.");
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ServerConfiguration fm1 = new ServerConfiguration();
            if (!ClusterStatus[2])
            {
                fm1.ShowDialog();
                if (fm1.Cluster != null)
                {
                    Clusters[2] = fm1.Cluster;
                    Comm comm = new Comm(Clusters[2]);
                    ClusterStatus[2] = comm.CheckStatus();
                    if (ClusterStatus[2])
                    {
                        this.pictureBox3.Image = global::Medical_Record_System.Properties.Resources.server_online;
                        this.pictureBox3.Invalidate();
                        textBox1.AppendText("Server 3 is Online.");
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            RecordSample rs = new RecordSample();
            rs.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void recordExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordSample rs = new RecordSample();
            rs.Show();
        }
    }
}
