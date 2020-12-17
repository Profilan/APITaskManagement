using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class TransMissionLine
    {
        public virtual int id { get; set; }
        public virtual Int64 nrcollo { get; set; }
        public virtual string vrzenh { get; set; }
        public virtual float gewicht { get; set; }
        public virtual float lengte { get; set; }
        public virtual float breedte { get; set; }
        public virtual float hoogte { get; set; }
        public virtual string referentie { get; set; }

        public virtual TransMissionHeader header { get; set; }
    }
}
