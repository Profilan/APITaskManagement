using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class EazystockForecast : Entity<Guid>
    {
        public virtual string Rowtransactiontype { get; set; }
        public virtual string Itemcode { get; set; }
        public virtual string Prefix { get; set; }
        public virtual string Warehousecode { get; set; }
        public virtual string Warehousegroupcode { get; set; }
        public virtual string Period { get; set; }
        public virtual string  ForecastAdjustmentType { get; set; }
        public virtual string Extractiondate { get; set; }
        public virtual string AdjustmentValueType { get; set; }
        public virtual float AdjustmentValue { get; set; }
        public virtual string ExcludeDemand { get; set; }
        public virtual string Comment { get; set; }

        public EazystockForecast()
        {

        }
    }
}
