using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_System
{
    public static class RecordComparison
    {

        public static bool CompareRecords(ClusterInfo cluster1, ClusterInfo cluster2, ClusterInfo cluster3, System.Windows.Forms.ToolStripProgressBar bar)
        {
            if (cluster1 == null || cluster2 == null || cluster3 == null)
                return false;

            bar.Value = 0;
            bar.Maximum = 4;
            bar.Step = 1;
            bar.Visible = true;

            bar.PerformStep();
            Dictionary<string, PatientRecord> recordsCluster1 = GetRecordFromCluster(cluster1);

            bar.PerformStep();
            Dictionary<string, PatientRecord> recordsCluster2 = GetRecordFromCluster(cluster2);
            bar.PerformStep();
            Dictionary<string, PatientRecord> recordsCluster3 = GetRecordFromCluster(cluster3);

            
            if (recordsCluster1.Count() != recordsCluster2.Count() || recordsCluster2.Count() != recordsCluster3.Count())
                return false;
            

            string key = "";
            PatientRecord record1, record2, record3;
            foreach(var item in recordsCluster1)
            {
                key = item.Key;
                record1 = item.Value;
                record2 = recordsCluster2[key];
                record3 = recordsCluster3[key];

                

                if (record2 == null || record3 == null)
                    return false;

                if (record1.Compare(record2) && record2.Compare(record3))
                    continue;
                else
                {
                    bar.Visible = false;
                    return false;
                }
                    
            }
            bar.PerformStep();
            bar.Visible = false;
            return true;
        }

        public static Dictionary<string, PatientRecord> GetRecordFromCluster(ClusterInfo cluster)
        {
            Dictionary<string, PatientRecord> result = new Dictionary<string, PatientRecord>();
            
            Comm comm = new Comm(cluster.username, cluster.password, cluster.server, cluster.keyspace);
            comm.Connect();
            ISession session = comm.GetSession();

            string queryString = "SELECT key, firstName, lastName, gender, date_of_birth, phone_no, address, xray_image_len, mri_image_len, diagnosis_pdf_len, date, comment FROM patient_record";
            PreparedStatement preparedStatement = session.Prepare(queryString);
            BoundStatement boundStatement = preparedStatement.Bind();
            RowSet cursors = session.Execute(boundStatement);

            string key = "";
            PatientRecord record = new PatientRecord();

            foreach (Row row in cursors)
            {
                key = row[0].ToString();
                record.FirstName = row[1].ToString();
                record.LastName = row[2].ToString();
                record.Gender = (GenderType)row[3];
                record.DateOfBirth = DateTime.Parse(row[4].ToString());
                record.Telephone = row[5].ToString();
                record.Address = row[6].ToString();
                record.XrayImageContentLen = (int)row[7];
                record.MRIImageContentLen = (int)row[8];
                record.DiagnosisPDFContentLen = (int)row[9];
                record.Date = DateTime.Parse(row[10].ToString());
                record.Comments = row[11].ToString();

                result.Add(key, record);
            }
            comm.Close();

            return result;
        }
    }
}
