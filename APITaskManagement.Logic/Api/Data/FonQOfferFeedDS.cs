using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class FonQOfferFeedDS
    {
        public virtual Guid Id { get; set; }
        public virtual string Debtor { get; set; }
        public virtual string EAN { get; set; }
        public virtual float MOQ { get; set; }
        public virtual string ArticleStatus { get; set; }
        public virtual string VATPercentage { get; set; }
        public virtual string BrandSupplier { get; set; }
        public virtual string ArticleDescription { get; set; }
        public virtual string ArticleNumberSupplier { get; set; }
        public virtual bool Fragile { get; set; }
        public virtual int DeliveryLeadTime { get; set; }
        public virtual float Weight { get; set; }
        public virtual float Length { get; set; }
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }
        public virtual int QuantityPerBox { get; set; }
        public virtual float QuantityPerOuterCarton { get; set; }
        public virtual float QuantityPerPallet { get; set; }
        public virtual int QuantityFreeOnStock { get; set; }
        public virtual DateTime RestockDate { get; set; }
        public virtual string ArticlePrice { get; set; }
    }
}
