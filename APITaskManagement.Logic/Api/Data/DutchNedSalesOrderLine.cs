using System;

namespace APITaskManagement.Logic.Api.Data
{
    public class DutchNedSalesOrderLine
    {
        public virtual int Id { get; set; }

        public virtual DateTime CollectionDate { get; set; }
        public virtual string Description { get; set; }
        public virtual string EANCode { get; set; }
        public virtual string Identifier { get; set; }
        public virtual string MainPackageIdentifier { get; set; }
        public virtual int Volume { get; set; }
        public virtual DateTime PlanFromDate { get; set; }
        public virtual string Warehouse { get; set; }
        public virtual int CashOnDelivery { get; set; }
        public virtual bool IsReturn { get; set; }

        public virtual DutchNedSalesOrder SalesOrder { get; set; }
    }
}
