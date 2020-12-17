using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DHLDeliveryLine
    {
        public virtual int Id { get; set; } // LineId
        public virtual DHLDeliveryLineIdentifier DeliveryLineIdentifier { get; set; }
        public virtual string OrderNr { get; set; }
        public virtual string CatalogNr { get; set; }
        public virtual string ProductNr { get; set; }
        public virtual string ProductName { get; set; }
        public virtual float Quantity { get; set; }
        public virtual float VolumeAmount { get; set; }
        public virtual string VolumeUnit { get; set; }
        public virtual int WeightAmount { get; set; }
        public virtual string WeightUnit { get; set; }

        public virtual DHLDeliveryHeader DeliveryHeader { get; set; }
        public virtual ISet<DHLDeliveryBarcode> Barcodes { get; set; }

        // EF requires an empty constructor
        protected DHLDeliveryLine()
        {

        }

        protected DHLDeliveryLine(int id)
        {
            this.Id = id;
        }

    }
}
