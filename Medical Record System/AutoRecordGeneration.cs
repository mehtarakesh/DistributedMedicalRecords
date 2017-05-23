using Cassandra;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_System
{
    public class AutoRecordGeneration
    {
        public List<Tuple<string, string>> FemaleNames = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> MaleNames = new List<Tuple<string, string>>();
        string[] cities = { "Bartlett", "Brentwood", "Bristol", "Chattanooga", "Clarksville", "Cleveland", "Columbia", "Cookeville", "Dyersburg", "East Ridge", "Franklin", "Gallatin", "Germantown", "Goodlettsville", "Hendersonville", "Jackson", "Johnson City", "Kingsport", "Knoxville", "La Vergne", "Lebanon", "Maryville", "Memphis", "Morristown", "Mount Juliet", "Murfreesboro", "Nashville", "Oak Ridge", "Sevierville", "Shelbyville", "Springfield", "Spring Hill", "Tullahoma" };
        string[] streets = { "Aaron Drive", "Aaronwood Court", "Alhambra Circle", "Alvin Sperry Pass", "Alvinwood Drive", "Alyadar Drive", "Amalie Court", "Amalie Drive", "Amanda Avenue", "Amanda Meadow Court", "Amber Court", "Amber Crest Court", "Amber Hills Lane", "Amberwood Circle", "Amberwood Place", "Ambrose Avenue", "Baldwin Court", "Balmoral Drive", "Balmy Avenue", "Baltic Drive", "Banbury Crossing", "Banbury Drive", "Bank Alley", "Bank Street", "Banshire Court", "Baptist World Center", "Baptist World Center Drive", "Barbara Lynn Way", "Barclay Drive", "Bell Forge Parkway", "Bell Forge Road", "Bell Grimes Lane", "Bernard Avenue", "Bernard Circle", "Bernard Road", "Berrien Street", "Berry Hill Drive", "Berry Road", "Berrywood Drive", "Berrywood Road", "Berrywood Way", "Bessie Avenue", "Beth Drive", "Bethany Boulevard", "Bethwood Drive", "Bettie Drive", "Betty Nichols Place", "Bexhill Court South", "Bianco Road", "Bidwell Road", "Brentview Hills Drive", "Brentwood Commons Way", "Brentwood Drive East", "Brentwood Highlands Drive", "Brentwood Meadows Circle", "Brentwood Place", "Brentwood Square", "Brentwood Terrace", "Brentwood Trace", "Bresslyn Court", "Bresslyn Road", "Bret Ridge Drive", "Brevard Court", "Brevity Lane", "Brewbock Court", "Brewer Court", "Brewer Drive", "Brian Circle", "Burton Valley Road", "Burwick Place", "Bush Road", "Bushnell Street", "Butler Circle", "Butler Road", "Buttercup Drive", "Butterfly Court", "Buttonquail Court", "Byrne Drive", "Campbell Avenue", "Campbell Circle", "Campbell Drive", "Campbell Street", "Campbell Street", "Campton Road", "Cedar Point Parkway", "Cedar Pointe Parkway", "Cedar Ridge Road", "Cedar Rock Drive", "Cedar Springs Drive", "Cedarcliff Road", "Cedarcreek Circle", "Cedarcreek Court", "Cedarcreek Creek Circle", "Cedarcreek Drive", "Cedarcreek Place", "Cedarcreek Trail", "Cedarcrest Avenue", "Cedarhill Drive", "Cedarmont Circle", "Cedarmont Court", "Cedarmont Drive", "Cedarstone Way", "Chesney Court", "Chesney Glen Drive", "Chessington Court", "Chessington Drive", "Chester Avenue", "Chesterfield Avenue", "Chestnut Street", "Chestnutwood Trail", "Cheswick Court", "Chet Atkins Place", "Chicamauga Avenue", "Chickadee Circle", "Chickasaw Avenue", "Chickering Circle", "Chickering Circle", "Chickering Lane", "Chickering Park Drive", "Chickering Road", "Chickering Woods Drive", "Chilton Street", "Chimney Hill", "Chipmunk Lane", "Clearbrook Drive", "Clearlake Drive East", "Clearlake Drive West", "Clearview Avenue", "Clearview Drive", "Clearwater Court", "Clearwater Drive", "Cleeces Ferry Road", "Cleghorn Avenue", "Clematis Drive", "Conway Street", "Cooper Lane", "Cooper Terrace", "Coopertown Road", "Copeland Drive", "Copper Ridge Trail", "Coral Court", "Coral Road", "Coral Way", "Corbett Lane", "Cordell Drive", "Corder Drive", "Cornelia Court", "Cornelia Street", "Cornerstone Court", "Cornet Drive", "Corning Drive", "Cornish Drive", "Cornwall Avenue", "Cornwall Court", "Cornwall Drive", "Coronado Court", "Croley Court", "Croley Drive", "Crosby Drive", "Cross Creek Road", "Cross Street", "Cross Timbers Drive", "Crossbrooke Drive", "Crossfield Drive", "Crossings Boulevard", "Crosswind Drive", "Crosswind Place", "Crosswood Court", "Crosswood Drive", "Crouch Drive", "Crowder Court", "Crowe Drive", "Crown Point Place", "Crownhill Drive", "Crows Nest Alley", "Curtiswood Lane North", "Cynthia Lane", "Cypress Drive", "D Ville Drive", "Dabbs Court", "Dade Drive", "Dahlgreen Court", "Dahlia Circle", "Dahlia Drive", "Daisy Trail", "Dakota Avenue", "Dale Avenue", "Dalebrook Court", "Dalebrook Lane", "Dalemere Court", "Dalemere Drive", "Dallas Avenue", "Dan Kestner Court", "Dan Kestner Drive", "Danbrook Drive", "Danbury Condominiums", "Danbury Court", "Danby Drive", "Devon Close", "Dolan Road", "Dolcetto Grove", "Dove Valley Court", "Dove Valley Drive", "Dovecote Drive", "Earlene Drive", "Earlington Drive", "Early Avenue", "Earps Court", "East Ashland Drive", "East Bend Drive", "East Brookfield Avenue", "East Chase Court", "East Chase Court", "East Colony Court", "East Colony Drive", "East Colony Place", "East Fairview Drive", "Eden Avenue", "Eden Street", "Edencrest Court", "Edenwold City", "Edenwold Road Con", "Edgar Street", "Edge Moor Drive", "Edge O Lake Drive", "Edgehill Avenue", "Edgemont Drive", "Edgeview Drive", "Free Silver Road", "Freedom Court", "Freedom Drive", "Freedom Place", "Freeland Station Road", "Freightliner Drive", "Fremont Avenue", "French Landing Drive", "Freno Lane", "Fresno Avenue", "Frierson Street", "Frisco Avenue", "Frist Boulevard", "Frith Drive", "Frontage Road", "Gaywinds Court", "Gaywood Drive", "Goodwin Court", "Goodwin Drive", "Grinstead Place", "Grissom Drive", "Grizzard Avenue", "Groome Drive", "Grosse Point Court", "Grove Creek Avenue", "Grover Street", "Groves Park Road", "Grubbs Road", "Grundy Court", "Grundy Street", "Guaranty Drive", "Guest Drive", "Guill Court", "Gulf Coast Court", "Gullett Drive", "Gun Club Road", "Hedrick Street", "Heidi Court", "Heiman Street", "Helena Bay Court", "Helmwood Drive", "Hemingway Drive", "Hemlock Avenue", "Hemstead Street", "Henderson Drive", "Heney Drive", "Henry Ford Drive", "Hydes Ferry Pike", "Hydes Ferry Road", "Hydesdale Lane", "Hynes Street" };

        public void ReadFemaleNames()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\female_names.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            char[] delimiterChars = { ' ', ',', '\t' };

            foreach (string line in lines)
            {
                string[] names = line.Split(delimiterChars);
                FemaleNames.Add(new Tuple<string, string>(names[0], names[1]));
            }
        }

        public void ReadMaleNames()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\male_names.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            char[] delimiterChars = { ' ', ',', '\t' };
            foreach (string line in lines)
            {
                string[] names = line.Split(delimiterChars);
                MaleNames.Add(new Tuple<string, string>(names[0], names[1]));
            }
        }

        public string GetRandomAddress()
        {
            Random rand = new Random();
            int n = rand.Next(0, streets.Length);
            int m = rand.Next(0, cities.Length);

            return rand.Next(0, 1000) + " " + streets[n] + ", " + cities[m] + ", " + "TN 37" + String.Format("{0:D3}", rand.Next(0, 1000));
        }

        public DateTime GetRandomDOB(DateTime from, DateTime to)
        {
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(new Random().NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }

        public string GetRandomTelNo()
        {
            StringBuilder telNo = new StringBuilder(12);
            int number;
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                number = rand.Next(1, 8);
                telNo = telNo.Append(number.ToString());
            }

            telNo = telNo.Append("-");

            number = rand.Next(0, 1000);
            telNo = telNo.Append(String.Format("{0:D3}", number));

            telNo = telNo.Append("-");

            number = rand.Next(0, 10000);
            telNo = telNo.Append(String.Format("{0:D4}", number));

            return telNo.ToString();
        }

        public void Initialization()
        {
            ReadFemaleNames();
            ReadMaleNames();
        }

        public void DynamicImage(string path, string content)
        {
            using (Bitmap image = new Bitmap(400, 300))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    string text = content;

                    Font drawFont = new Font("Arial", 20);
                    SolidBrush drawBrush = new SolidBrush(Color.Red);
                    for (int i = 50; i < 300; i+=100)
                    {
                        PointF stringPonit = new PointF(0, i);
                        g.DrawString(text, drawFont, drawBrush, stringPonit);
                    }
                    
                }

                image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        //Constructor
        public AutoRecordGeneration()
        {

        }
        public void GenerateRecords(ClusterInfo cluster, int numebrOfRecords, System.Windows.Forms.ToolStripProgressBar bar)
        {
            Initialization();
            int iteration = 0;
            int mc = 0;
            int fc = 0;

            Random rand = new Random();
            PatientRecord record = new PatientRecord();

            Comm comm = new Comm(cluster.username, cluster.password, cluster.server, cluster.keyspace);
            comm.Connect();
            ISession session = comm.GetSession();

            //Deleting existing data in the patient_record
            string truncateString = "truncate patient_record";
            PreparedStatement ps = session.Prepare(truncateString);
            BoundStatement bs = ps.Bind();
            session.Execute(bs);

            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            while (iteration < numebrOfRecords)
            {
                if((fc >= 500) || (rand.Next(0, 2) == 0 && mc < 500))
                {
                    record.FirstName = MaleNames.ElementAt(mc).Item1;
                    record.LastName = MaleNames.ElementAt(mc).Item2;
                    record.Gender = GenderType.Male;
                    mc++;
                }else
                {
                    record.FirstName = FemaleNames.ElementAt(fc).Item1;
                    record.LastName = FemaleNames.ElementAt(fc).Item2;
                    record.Gender = GenderType.Femal;
                    fc++;
                }
                record.DateOfBirth = GetRandomDOB(new DateTime(1950, 1, 1), DateTime.Today);
                record.Telephone = GetRandomTelNo();
                record.Address = GetRandomAddress();
                record.Date = DateTime.Now;
                record.Comments = "This is for future use.";

                string xray_path = Path.Combine(basePath, @"Resources\" + record.LastName + "_" + record.Telephone + "_xray" + ".png");
                string mri_path = Path.Combine(basePath, @"Resources\" + record.LastName + "_" + record.Telephone + "_mri" + ".png");
                string diagnosis_path = Path.Combine(basePath, @"Resources\" + record.LastName + "_" + record.Telephone + "_diagnosis" + ".pdf");

                //Creating xray image
                DynamicImage(xray_path, record.GetString());

                //Creating mri image
                DynamicImage(mri_path, record.GetString());

                //Creating diagnosis pdf file
                var writer = new PdfWriter(diagnosis_path);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                for (int k = 0; k < 5; k++)
                    document.Add(new Paragraph(record.GetString()));
                document.Close();

                record.XrayImageContent = ReadFile(xray_path);
                record.MRIImageContent = ReadFile(mri_path);
                record.DiagnosisPDFContent = ReadFile(diagnosis_path);

                string queryString = "INSERT INTO Patient_Record(key, firstName, lastName, gender, date_of_birth, phone_no, address, xray_image, xray_image_len, mri_image, mri_image_len, diagnosis_pdf, diagnosis_pdf_len, date, comment)"
                    + " VALUES(now(), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                PreparedStatement preparedStatement = session.Prepare(queryString);
                BoundStatement boundStatement = preparedStatement.Bind(
                    record.FirstName,
                    record.LastName,
                    (int)record.Gender,
                    record.DateOfBirth.ToShortDateString(),
                    record.Telephone,
                    record.Address,
                    record.XrayImageContent,
                    record.XrayImageContent.Length,
                    record.MRIImageContent,
                    record.MRIImageContent.Length,
                    record.DiagnosisPDFContent,
                    record.DiagnosisPDFContent.Length,
                    DateTime.Now.ToShortDateString(),
                    record.Comments
                    );
                session.Execute(boundStatement);

                //delete creating files after storing in the database
                File.Delete(xray_path);
                File.Delete(mri_path);
                File.Delete(diagnosis_path);

                iteration++;
                bar.PerformStep();
            }

            comm.Close();
            bar.Visible = false;
            
        }

        public bool ModifyRecord(ClusterInfo cluster, int numberOfRecords, System.Windows.Forms.ToolStripProgressBar bar)
        {
            Comm comm = new Comm(cluster.username, cluster.password, cluster.server, cluster.keyspace);
            comm.Connect();
            ISession session = comm.GetSession();

            string queryString = "SELECT key FROM patient_record LIMIT " + numberOfRecords;

            PreparedStatement preparedStatement = session.Prepare(queryString);
            BoundStatement boundStatement = preparedStatement.Bind();
            RowSet result = session.Execute(boundStatement);
            
            foreach (Row row in result)
            {
                string updateString = "UPDATE patient_record SET phone_no='" 
                    + GetRandomTelNo() + "', address='" 
                    + GetRandomAddress() + "' WHERE key=" 
                    + row[0].ToString();
                PreparedStatement ps = session.Prepare(updateString);
                BoundStatement bs = ps.Bind();
                session.Execute(bs);
                bar.PerformStep();
            }
            bar.Visible = false;
            return true;
        }
    }
}