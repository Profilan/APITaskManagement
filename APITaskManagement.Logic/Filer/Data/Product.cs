using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class Product
    {
        public virtual string ProductCode { get; set; }                 // ARTIKELCODE
        public virtual string EANCode { get; set; }                     // EANCODE
        public virtual string Assortment { get; set; }                  // ASSORTMENT
        public virtual string Productgroup { get; set; }                // PRODUCTGROUP
        public virtual string ProductRange { get; set; }                // RANGE
        public virtual string Brand { get; set; }                       // BRAND
        public virtual string ProductNameNL { get; set; }               // NL
        public virtual string ProductNameEN { get; set; }               // EN
        public virtual string ProductNameDE { get; set; }               // DE
        public virtual string ProductNameFR { get; set; }               // FR
        public virtual string DescriptionNL { get; set; }               // NL_LONG_DESC
        public virtual string DescriptionEN { get; set; }               // EN_LONG_DESC            
        public virtual string DescriptionDE { get; set; }               // DE_LONG_DESC
        public virtual string DescriptionFR { get; set; }               // FR_LONG_DESC
        public virtual string Material { get; set; }                    // MATERIAL
        public virtual string Color { get; set; }                       // COLOR_FINISH
        public virtual string SizeCm { get; set; }                      // AFMETING_ARTIKEL_HXBXD
        public virtual bool FSC { get; set; }                           // FSC
        public virtual string SalesUnit { get; set; }                   // VERKOOPEENHEID
        public virtual decimal SalesPriceEuro { get; set; }             // VERKOOPPRIJS
        public virtual string Status { get; set; }                      // STATUS
        public virtual bool NewArrival { get; set; }                    // NEW_ARRIVAL
        public virtual bool OnlineActivation { get; set; }              // ONLINE_ACTIVATIE
        public virtual int QtyPackages { get; set; }                    // AANTAL_PAKKETTEN
        public virtual decimal OrderingQty { get; set; }                // COMMERCIELEBE
        public virtual decimal FreeStockQty { get; set; }               // VrijeVoorraad
        public virtual decimal FreeStockQtyChannelEngine { get; set; }  // VrijeVoorraadChannelEngine
        public virtual bool AssemblyRequired { get; set; }              // ASSEMBLY_REQUIRED
        public virtual string CountryOfOrigin { get; set; }             // COUNTRY_OF_ORIGIN
        public virtual string IntrastatCode { get; set; }               // INTRASTAT_CODE
        public virtual string Exclusivity { get; set; }                 // EXCLUSIV
        public virtual string ShippingMethod { get; set; }              // VERZENDWIJZE
        public virtual string ShippingMethod2 { get; set; }             // SHIPPINGMETHOD2
        public virtual decimal WeightKg { get; set; }                   // GEWICHT
        public virtual decimal ConsumerPriceNL { get; set; }            // CONSUMENTENPRIJS
        public virtual decimal ConsumerPriceFromNL { get; set; }        // CONSUMENTENPRIJS_VAN
        public virtual decimal ConsumerPriceISE { get; set; }           // ISE_CONSUMENTENPRIJS
        public virtual decimal ConsumerPriceFromISE { get; set; }       // ISE_CONSUMENTENPRIJS_VAN
        public virtual string Condition { get; set; }                   // Condition
        public virtual decimal DepthMm { get; set; }                    // VERPAK_DIKTE_mm
        public virtual decimal LengthMm { get; set; }                   // VERPAK_LENGTE_mm
        public virtual decimal WidthMm { get; set; }                    // VERPAK_BREEDTE_mm
        public virtual decimal VolumeM3 { get; set; }                   // VOL_M3_VERP
        public virtual string Warehouse { get; set; }                   // WAREHOUSE
        public virtual string ProductManual { get; set; }               // ProductManual
        public Product()
        {
            
           
        }
    }
}
