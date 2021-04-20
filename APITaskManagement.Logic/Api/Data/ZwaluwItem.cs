using APITaskManagement.Logic.Common;
using System;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwItem : Entity<int>
    {
        public virtual Guid ItemId { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual string Description { get; set; }

        public virtual string Supplier { get; set; }

        public virtual string EANCode { get; set; }

        public virtual float Weight { get; set; }

        public virtual float Volume { get; set; }

        public virtual float Height { get; set; }

        public virtual float Length { get; set; }

        public virtual float Width { get; set; }

        public virtual float SalesUnitPerColliUnit { get; set; }
    }
}
