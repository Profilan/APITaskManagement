using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer
{
    public abstract class FilerFormatterAbstract : IFilerFormatter
    {
        public ContentFormat Format { get; set; }
        public string Destination { get; set; }

        public FilerFormatterAbstract(ContentFormat format)
        {
            Format = format;
        }

        public bool saveContent(string destination)
        {
            Destination = destination;

            {
                switch (Format)
                {
                    case ContentFormat.JSON:
                        return saveJSONContent();
                    case ContentFormat.XML:
                        return saveXMLContent();
                    case ContentFormat.POS:
                        return savePOSContent();
                }

                return false;
            }
        }

        public abstract bool saveJSONContent();

        public abstract bool saveXMLContent();

        public abstract bool savePOSContent();

        public IList<string> getContent(int key = -1)
        {
            switch (Format)
            {
                case ContentFormat.JSON:
                    return getJSONContent(key);
                case ContentFormat.XML:
                    return getXMLContent(key);
                case ContentFormat.POS:
                    return getPOSContent(key);
            }

            return null;
        }

        public abstract IList<string> getXMLContent(int key = -1);

        public abstract IList<string> getJSONContent(int key = -1);

        public abstract IList<string> getPOSContent(int key = -1);
    }
}
