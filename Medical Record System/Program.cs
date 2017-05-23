using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medical_Record_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*ClusterInfo cluster1 = new ClusterInfo("dsproject", "dsproject", "192.168.0.103", "demo");

            AutoRecordGeneration gen = new AutoRecordGeneration();
            gen.GenerateRecords(cluster1, 1000);
            gen.ModifyRecord(cluster1, 100);

            bool consistent = RecordComparison.CompareRecords(cluster1, cluster1, cluster1);
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }
    }
}
