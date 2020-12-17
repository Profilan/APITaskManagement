using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DHLDeliveryBarcode 
    {
        public virtual int Id { get; set; }
        
        public virtual string Barcode { get; set; }

        public virtual DHLDeliveryLine DeliveryLine { get; set; }

    }
}
