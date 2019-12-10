using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class Package
    {
        public virtual string PackageCode { get; set; }             // ARTCODE_PAKKET
        public virtual string ProductCode { get; set; }            // ARTIKELCODE
        public virtual string EANCode { get; set; }                // EANCODE
        public virtual string ProductNameNL { get; set; }          // NL
        public virtual string ProductNameEN { get; set; }          // EN
        public virtual string ProductNameDE { get; set; }          // DE
        public virtual string ProductNameFR { get; set; }          // FR
        public virtual decimal WeightKg { get; set; }              // GEWICHT
        public virtual decimal DepthMm { get; set; }               // VERPAK_DIKTE_mm
        public virtual decimal LengthMm { get; set; }              // VERPAK_LENGTE_mm
        public virtual decimal WidthMm { get; set; }               // VERPAK_BREEDTE_mm
        public virtual decimal VolumeM3 { get; set; }              // VOL_M3_VERP
        public virtual decimal Quantity { get; set; }              // AANTAL_PAKKETTEN
        public virtual string ShippingMethod2 { get; set; }        // SHIPPINGMETHOD2 
        public virtual string Warehouse { get; set; }              // WAREHOUSE
        public virtual Product Product { get; set; }

        public Package()
        {

        }
    }
}
