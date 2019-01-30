using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Data
{
    public class DutchNedSalesOrder
    {
        public virtual int Id { get; set; }

        public virtual string OrderNumber { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual string OrderReference { get; set; }
        public virtual string DeliveryInstructions { get; set; }
        public virtual bool IsCombyOrder { get; set; }
        // public virtual bool MailCustomer { get; set; }
        public virtual DutchNedCustomer Customer { get; set; }
        public virtual DutchNedSender Sender { get; set; }

        public virtual ISet<DutchNedSalesOrderLine> Lines { get; set; }

        public DutchNedSalesOrder()
        {
            Lines = new HashSet<DutchNedSalesOrderLine>();
        }
    }
}
