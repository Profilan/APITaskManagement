using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APITaskManagement.Logic.Filer.Formatters
{
    public class TransmissionFormatter : FilerFormatterAbstract
    {
        private readonly TransMissionRepository transMissionRepository = new TransMissionRepository();
        public TransmissionFormatter(ContentFormat format) : base(format)
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

        public override IList<string> GetXMLContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public XmlElement getXMLContent(XmlDocument doc, int key = -1)
        {
            try
            {
                var item = transMissionRepository.GetById(key);

                XmlElement opdracht = doc.CreateElement("oOpdracht");

                opdracht.AppendChild(doc.CreateElement("type")).AppendChild(doc.CreateTextNode(item.type));
                opdracht.AppendChild(doc.CreateElement("nrorder")).AppendChild(doc.CreateTextNode(item.nrorder));
                opdracht.AppendChild(doc.CreateElement("datum")).AppendChild(doc.CreateTextNode(item.datum.ToString("dd-MM-yyyy")));

                opdracht.AppendChild(doc.CreateElement("afznaam")).AppendChild(doc.CreateTextNode(item.afznaam));
                opdracht.AppendChild(doc.CreateElement("afznaam2"));
                opdracht.AppendChild(doc.CreateElement("afzstraat"));
                opdracht.AppendChild(doc.CreateElement("afzhuisnr"));
                opdracht.AppendChild(doc.CreateElement("afzpostcode"));
                opdracht.AppendChild(doc.CreateElement("afzplaats"));
                opdracht.AppendChild(doc.CreateElement("afzland"));

                opdracht.AppendChild(doc.CreateElement("geanaam")).AppendChild(doc.CreateTextNode(item.geanaam));
                opdracht.AppendChild(doc.CreateElement("geanaam2"));
                opdracht.AppendChild(doc.CreateElement("geastraat")).AppendChild(doc.CreateTextNode(item.geastraat));
                opdracht.AppendChild(doc.CreateElement("geahuisnr")).AppendChild(doc.CreateTextNode(item.geahuisnr));
                opdracht.AppendChild(doc.CreateElement("geapostcode")).AppendChild(doc.CreateTextNode(item.geapostcode));
                opdracht.AppendChild(doc.CreateElement("geaplaats")).AppendChild(doc.CreateTextNode(item.geaplaats));
                opdracht.AppendChild(doc.CreateElement("gealand")).AppendChild(doc.CreateTextNode(item.gealand));

                opdracht.AppendChild(doc.CreateElement("geatelefoon")).AppendChild(doc.CreateTextNode(item.geatelefoon));
                opdracht.AppendChild(doc.CreateElement("geaemail")).AppendChild(doc.CreateTextNode(item.geaemail));
                opdracht.AppendChild(doc.CreateElement("instructie"));
                opdracht.AppendChild(doc.CreateElement("rembours"));

                // XmlElement aPlus = doc.CreateElement("aPlus");

                XmlElement aRegel = doc.CreateElement("aRegel");
                opdracht.AppendChild(aRegel);

                foreach (var line in item.lines)
                {
                    XmlElement regelItem = doc.CreateElement("item");

                    regelItem.AppendChild(doc.CreateElement("nrcollo")).AppendChild(doc.CreateTextNode(line.nrcollo.ToString()));
                    regelItem.AppendChild(doc.CreateElement("vrzenh")).AppendChild(doc.CreateTextNode(line.vrzenh));
                    regelItem.AppendChild(doc.CreateElement("gewicht")).AppendChild(doc.CreateTextNode(line.gewicht.ToString()));
                    regelItem.AppendChild(doc.CreateElement("lengte")).AppendChild(doc.CreateTextNode(line.lengte.ToString()));
                    regelItem.AppendChild(doc.CreateElement("breedte")).AppendChild(doc.CreateTextNode(line.breedte.ToString()));
                    regelItem.AppendChild(doc.CreateElement("hoogte")).AppendChild(doc.CreateTextNode(line.hoogte.ToString()));
                    regelItem.AppendChild(doc.CreateElement("referentie")).AppendChild(doc.CreateTextNode(line.referentie));
                    regelItem.AppendChild(doc.CreateElement("omsverp"));
                    regelItem.AppendChild(doc.CreateElement("omruilen"));

                    aRegel.AppendChild(regelItem);

                }

                return opdracht;
            }
            catch (Exception)
            {
                return null;
            }
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
            throw new NotImplementedException();
        }
    }
}
