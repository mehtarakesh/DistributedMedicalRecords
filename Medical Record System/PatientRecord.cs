using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_System
{
    public enum GenderType { Male, Femal};
    public class PatientRecord
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Telephone { get; set; }
        public String Address { get; set; }
        public byte[] XrayImageContent { get; set; }
        public int XrayImageContentLen { get; set; }
        public byte[] MRIImageContent { get; set; }
        public int MRIImageContentLen { get; set; }
        public byte[] DiagnosisPDFContent { get; set; }
        public int DiagnosisPDFContentLen { get; set; }
        public DateTime Date { get; set; }
        public String Comments { get; set; }

        public PatientRecord()
        {

        }

        public string GetString()
        {
            return FirstName + "," + LastName + "," + Gender.ToString() + "," + DateOfBirth.ToShortDateString() + "," + Telephone + "," + Address + "," + XrayImageContentLen + "," + MRIImageContentLen + "," + DiagnosisPDFContentLen + "," + Date + "," + Comments;
        }

        public bool Compare(PatientRecord ob)
        {
            if(!NEQ(FirstName, ob.FirstName) ||
                !NEQ(LastName, ob.LastName) ||
                Gender != ob.Gender ||
                !NEQ(DateOfBirth.ToShortDateString(), ob.DateOfBirth.ToShortDateString()) ||
                !NEQ(Telephone, ob.Telephone) ||
                !NEQ(Address, ob.Address) ||
                XrayImageContentLen != ob.XrayImageContentLen ||
                MRIImageContentLen != ob.MRIImageContentLen ||
                DiagnosisPDFContentLen != ob.DiagnosisPDFContentLen ||
                !NEQ(Date.ToShortDateString(), ob.Date.ToShortDateString()) ||
                !NEQ(Comments, ob.Comments)){
                return false;
            }

            return true;
        }
        public bool NEQ(string a, string b)
        {
            if (a.Equals(b)) return true;
            else return false;
        }
    }
}
