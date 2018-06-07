using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class Pos
    {
        public virtual int AI1 { get; set; }        // Id
        public virtual string KNA { get; set; }     // Customer_Name
        public virtual string KVN { get; set; }     // ContactPerson
        public virtual string KST { get; set; }     // Customer_Address
        public virtual string KPL { get; set; }     // Customer_Pc
        public virtual string KOR { get; set; }     // Customer_City
        public virtual string KLA { get; set; }     // Customer_Country
        public virtual string KTM { get; set; }     // Customer_Phone
        public virtual string KEM { get; set; }     // Customer_Email
        public virtual DateTime KRD { get; set; }   // EntryDate
        public virtual string PSD { get; set; }     // Complaint
        public virtual DateTime KLD { get; set; }   // PurchaseDate
        public virtual string AI2 { get; set; }     // Reference
        public virtual string PMA { get; set; }     // ItemCode
        public virtual string PTL { get; set; }     // ItemDescription
        public virtual string PBA { get; set; }     // Material
        public virtual string AKN { get; set; }     // Client's POS Customer Number
    }
}
