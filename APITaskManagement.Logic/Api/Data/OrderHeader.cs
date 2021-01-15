using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class OrderHeader
    {
        public virtual int ID { get; set; }
        public virtual OrderIdentifier OrderIdentifier { get; set; }
        public virtual string OMSCHRIJVING { get; set; }
	    public virtual int STATUS { get; set; }
	    public virtual string ORDERNR { get; set; }
	    public virtual string SELECTIECODE { get; set; }
	    public virtual string ORDERTOELICHTING { get; set; }
	    public virtual int ACCEPTATIE_VERZAMELEN { get; set; }
	    public virtual int ACCEPTATIE_ORDERKOSTEN { get; set; }
	    public virtual string DS_NAAM { get; set; }
	    public virtual string DS_AANSPREEKTITEL { get; set; }
	    public virtual string DS_ADRES1 { get; set; }
	    public virtual string DS_POSTCODE { get; set; }
	    public virtual string DS_PLAATS { get; set; }
	    public virtual string DS_LAND { get; set; }
	    public virtual string DS_TELEFOON { get; set; }
	    public virtual string DS_EMAIL { get; set; }
        public virtual string SR_SERVICE_PRODUCT { get; set; }
        public virtual string SR_AFLEVEREN_AAN { get; set; }
        public virtual string SR_LOCATIE { get; set; }
        public virtual string SR_BEDRIJFSNAAM { get; set; }
        public virtual string SR_BEWIJS { get; set; }
        public virtual string SR_ORDERREF { get; set; }
        public virtual string SR_REDEN { get; set; }
        public virtual string SR_TOELICHTING { get; set; }
        public virtual string SR_PDF_ATTACHMENT { get; set; }
        public virtual string AUTHENTICATED_USER { get; set; }
	    public virtual int ACCEPTATIE_ORDERSPLITSING { get; set; }
	    public virtual int PAYMENT_RELEASE_REQUIRED { get; set; }
        public virtual DateTime SYSCREATED { get; set; }
        public virtual DateTime SYSMODIFIED { get; set; }
        public virtual string SYSMSG { get; set; }
        public virtual int CUSTOMERORDERID { get; set; }

        public virtual ISet<OrderLine> Lines { get; set; }

        public OrderHeader() : base()
        {
            Lines = new HashSet<OrderLine>();
        }
    }
}
