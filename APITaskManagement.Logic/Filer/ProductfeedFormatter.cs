using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace APITaskManagement.Logic.Filer
{
    public class ProductfeedFormatter : FilerFormatterAbstract
    {
        private readonly ProductRepository productRepository;
        private readonly ImageRepository imageRepository;
        private readonly PackageRepository packageRepository;

        public ProductfeedFormatter(ContentFormat format) : base(format)
        {
            productRepository = new ProductRepository();
            imageRepository = new ImageRepository();
            packageRepository = new PackageRepository();
        }

        public override IList<string> getJSONContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public override IList<string> getTXTContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public override IList<string> getXMLContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public override bool saveJSONContent()
        {
            throw new NotImplementedException();
        }

        public override bool saveTXTContent()
        {
            throw new NotImplementedException();
        }

        public override bool saveXMLContent()
        {
            var products = productRepository.List();

            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclariation = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclariation, root);

            XmlElement xmlProducts = doc.CreateElement(string.Empty, "Products", string.Empty);
            doc.AppendChild(xmlProducts);

            foreach (var product in products)
            {
                XmlElement xmlProduct = doc.CreateElement(string.Empty, "Product", string.Empty);
                xmlProducts.AppendChild(xmlProduct);
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "EANCode", string.Empty)).AppendChild(doc.CreateTextNode((product.EANCode)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductCode", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductCode)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Assortment", string.Empty)).AppendChild(doc.CreateTextNode((product.Assortment)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Productgroup", string.Empty)).AppendChild(doc.CreateTextNode((product.Productgroup)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductRange", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductRange)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Brand", string.Empty)).AppendChild(doc.CreateTextNode((product.Brand)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductNameNL", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameNL)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductNameEN", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameEN)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductNameDE", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameDE)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductNameFR", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameFR)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "DescriptionNL", string.Empty)).AppendChild(doc.CreateTextNode((product.DescriptionNL)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "DescriptionEN", string.Empty)).AppendChild(doc.CreateTextNode((product.DescriptionEN)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "DescriptionDE", string.Empty)).AppendChild(doc.CreateTextNode((product.DescriptionDE)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "DescriptionFR", string.Empty)).AppendChild(doc.CreateTextNode((product.DescriptionFR)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Material", string.Empty)).AppendChild(doc.CreateTextNode((product.Material)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Color", string.Empty)).AppendChild(doc.CreateTextNode((product.Color)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "SizeCm", string.Empty)).AppendChild(doc.CreateTextNode((product.SizeCm)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "FSC", string.Empty)).AppendChild(doc.CreateTextNode((Convert.ToString(product.FSC))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "SalesUnit", string.Empty)).AppendChild(doc.CreateTextNode((product.SalesUnit)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "SalesPriceEuro", string.Empty)).AppendChild(doc.CreateTextNode((product.SalesPriceEuro.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Status", string.Empty)).AppendChild(doc.CreateTextNode((product.Status)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "NewArrival", string.Empty)).AppendChild(doc.CreateTextNode((Convert.ToString(product.NewArrival))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "OnlineActivation", string.Empty)).AppendChild(doc.CreateTextNode((Convert.ToString(product.OnlineActivation))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "QtyPackages", string.Empty)).AppendChild(doc.CreateTextNode((product.QtyPackages.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "OrderingQty", string.Empty)).AppendChild(doc.CreateTextNode((product.OrderingQty.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "FreeStockQty", string.Empty)).AppendChild(doc.CreateTextNode((product.FreeStockQty.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "AssemblyRequired", string.Empty)).AppendChild(doc.CreateTextNode((Convert.ToString(product.AssemblyRequired))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "IntrastatCode", string.Empty)).AppendChild(doc.CreateTextNode((product.IntrastatCode)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Exclusivity", string.Empty)).AppendChild(doc.CreateTextNode((product.Exclusivity)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ShippingMethod", string.Empty)).AppendChild(doc.CreateTextNode((product.ShippingMethod)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ShippingMethod2", string.Empty)).AppendChild(doc.CreateTextNode((product.ShippingMethod2)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "WeightKg", string.Empty)).AppendChild(doc.CreateTextNode((product.WeightKg.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ConsumerPriceNL", string.Empty)).AppendChild(doc.CreateTextNode((product.ConsumerPriceNL.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ConsumerPriceFromNL", string.Empty)).AppendChild(doc.CreateTextNode((product.ConsumerPriceFromNL.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ConsumerPriceISE", string.Empty)).AppendChild(doc.CreateTextNode((product.ConsumerPriceISE.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ConsumerPriceFromISE", string.Empty)).AppendChild(doc.CreateTextNode((product.ConsumerPriceFromISE.ToString(new CultureInfo("en-US")))));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "Warehouse", string.Empty)).AppendChild(doc.CreateTextNode((product.Warehouse)));
                xmlProduct.AppendChild(doc.CreateElement(string.Empty, "ProductManual", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductManual)));
                XmlElement xmlPackages = doc.CreateElement(string.Empty, "Packages", string.Empty);
                xmlProduct.AppendChild(xmlPackages);
                if (product.QtyPackages > 1)
                {
                    var packages = packageRepository.ListByProductCode(product.ProductCode);
                    foreach (var package in packages)
                    {
                        XmlElement xmlPackage = doc.CreateElement(string.Empty, "Package", string.Empty);
                        xmlPackages.AppendChild(xmlPackage);
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "EANCode", string.Empty)).AppendChild(doc.CreateTextNode((package.EANCode)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductCode", string.Empty)).AppendChild(doc.CreateTextNode((package.PackageCode)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameNL", string.Empty)).AppendChild(doc.CreateTextNode((package.ProductNameNL)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameEN", string.Empty)).AppendChild(doc.CreateTextNode((package.ProductNameEN)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameDE", string.Empty)).AppendChild(doc.CreateTextNode((package.ProductNameDE)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameFR", string.Empty)).AppendChild(doc.CreateTextNode((package.ProductNameFR)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "WeightKg", string.Empty)).AppendChild(doc.CreateTextNode((package.WeightKg.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "DepthMm", string.Empty)).AppendChild(doc.CreateTextNode((package.DepthMm.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "LengthMm", string.Empty)).AppendChild(doc.CreateTextNode((package.LengthMm.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "WidthMm", string.Empty)).AppendChild(doc.CreateTextNode((package.WidthMm.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "VolumeM3", string.Empty)).AppendChild(doc.CreateTextNode((package.VolumeM3.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "Quantity", string.Empty)).AppendChild(doc.CreateTextNode((package.Quantity.ToString(new CultureInfo("en-US")))));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ShippingMethod2", string.Empty)).AppendChild(doc.CreateTextNode((package.ShippingMethod2)));
                        xmlPackage.AppendChild(doc.CreateElement(string.Empty, "Warehouse", string.Empty)).AppendChild(doc.CreateTextNode((package.Warehouse)));
                    }
                }
                else
                {
                    XmlElement xmlPackage = doc.CreateElement(string.Empty, "Package", string.Empty);
                    xmlPackages.AppendChild(xmlPackage);
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "EANCode", string.Empty)).AppendChild(doc.CreateTextNode((product.EANCode)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductCode", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductCode)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameNL", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameNL)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameEN", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameEN)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameDE", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameDE)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ProductNameFR", string.Empty)).AppendChild(doc.CreateTextNode((product.ProductNameFR)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "WeightKg", string.Empty)).AppendChild(doc.CreateTextNode((product.WeightKg.ToString(new CultureInfo("en-US")))));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "DepthMm", string.Empty)).AppendChild(doc.CreateTextNode((product.DepthMm.ToString(new CultureInfo("en-US")))));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "LengthMm", string.Empty)).AppendChild(doc.CreateTextNode((product.LengthMm.ToString(new CultureInfo("en-US")))));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "WidthMm", string.Empty)).AppendChild(doc.CreateTextNode((product.WidthMm.ToString(new CultureInfo("en-US")))));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "VolumeM3", string.Empty)).AppendChild(doc.CreateTextNode((product.VolumeM3.ToString(new CultureInfo("en-US")))));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "Quantity", string.Empty)).AppendChild(doc.CreateTextNode(("1")));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "ShippingMethod2", string.Empty)).AppendChild(doc.CreateTextNode((product.ShippingMethod2)));
                    xmlPackage.AppendChild(doc.CreateElement(string.Empty, "Warehouse", string.Empty)).AppendChild(doc.CreateTextNode((product.Warehouse)));
                }
                XmlElement xmlImages = doc.CreateElement(string.Empty, "Images", string.Empty);
                xmlProduct.AppendChild(xmlImages);
                var images = imageRepository.ListByEANCode(product.EANCode);
                foreach (var image in images)
                {
                    XmlElement xmlImage = doc.CreateElement(string.Empty, "Image", string.Empty);
                    xmlImages.AppendChild(xmlImage);
                    xmlImage.AppendChild(doc.CreateElement(string.Empty, "ImageUrl", string.Empty)).AppendChild(doc.CreateTextNode((image.ImageUrl)));
                }
            }


            try
            {
                doc.Save(Destination + ".xml");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
