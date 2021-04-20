using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class TransMissionHeader
    {
        public virtual int id { get; set; }
        public virtual string type { get; set; }
        public virtual string nrorder { get; set; }
        public virtual DateTime datum { get; set; }
        public virtual string afznaam { get; set; }
        public virtual string geanaam { get; set; }
        public virtual string geanaam2 { get; set; }
        public virtual string geastraat { get; set; }
        public virtual string geahuisnr { get; set; }
        public virtual string geapostcode { get; set; }
        public virtual string geaplaats { get; set; }
        public virtual string gealand { get; set; }
        public virtual string geatelefoon { get; set; }
        public virtual string geaemail { get; set; }

        public virtual ISet<TransMissionLine> lines { get; set; }

        public TransMissionHeader()
        {
            lines = new HashSet<TransMissionLine>();
        }
    }
}
