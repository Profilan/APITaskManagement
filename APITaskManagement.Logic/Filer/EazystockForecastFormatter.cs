using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APITaskManagement.Logic.Filer
{
    public class EazystockForecastFormatter : FilerFormatterAbstract
    {
        private readonly EazystockForecastRepository eazystockForecastRepository = new EazystockForecastRepository();

        public EazystockForecastFormatter(ContentFormat format) : base(format)
        {
            
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
            try
            {
                var items = eazystockForecastRepository.List();

                XmlDocument doc = new XmlDocument();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement xmlForecastAdjustment = doc.CreateElement(string.Empty, "FORECAST_ADJUSTMENT", string.Empty);
                doc.AppendChild(xmlForecastAdjustment);

                foreach (var forecast in items)
                {
                    XmlElement xmlForecast = doc.CreateElement(string.Empty, "FORECAST", string.Empty);
                    xmlForecastAdjustment.AppendChild(xmlForecast);
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "ROW_TYPE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Rowtransactiontype)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "ITEM_CODE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Itemcode)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "WAREHOUSE_CODE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Warehousecode)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "WAREHOUSE_GROUP_CODE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Warehousegroupcode)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "PERIOD", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Period)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "FORECAST_ADJUSTMENT_TYPE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.ForecastAdjustmentType)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "EXTRACTION_DATE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.Extractiondate)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "ADJUSTMENT_VALUE_TYPE", string.Empty)).AppendChild(doc.CreateTextNode((forecast.AdjustmentValueType)));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "ADJUSTMENT", string.Empty)).AppendChild(doc.CreateTextNode((forecast.AdjustmentValue.ToString())));
                    xmlForecast.AppendChild(doc.CreateElement(string.Empty, "EXCLUDE_DEMAND", string.Empty)).AppendChild(doc.CreateTextNode((forecast.ExcludeDemand)));
                }

                try
                {
                    doc.Save(Destination + ".txt");

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
