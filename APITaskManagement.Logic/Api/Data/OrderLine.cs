using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class OrderLine
    {
        public virtual int ID { get; set; }
        public virtual int ORDERHEADERID { get; set; }
        public virtual OrderIdentifier OrderIdentifier { get; set; }
        public virtual string ITEMCODE { get; set; }
	    public virtual float AANTAL { get; set; }
        public virtual float NETTO_PRIJS { get; set; }
        public virtual decimal NETT_PRICE_INCL_VAT { get; set; }
        public virtual decimal UNIT_VAT { get; set; }
        public virtual int STATUS { get; set; }
	    public virtual string ORDERNR { get; set; }
	    public virtual string VERZENDWEEK { get; set; }
        public virtual DateTime SYSCREATED { get; set; }
        public virtual DateTime SYSMODIFIED { get; set; }
        public virtual string SYSMSG { get; set; }
        public virtual DateTime? REQUESTEDDATE { get; set; }

        public OrderLine()
        {
            SYSCREATED = DateTime.Now;
            SYSMODIFIED = SYSCREATED;
        }
    }
}
