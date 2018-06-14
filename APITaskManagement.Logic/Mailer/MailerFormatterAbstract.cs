using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Mailer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Mailer
{
    public abstract class MailerFormatterAbstract : IMailerFormatter
    {
        public ContentFormat Format { get; set; }
 
        public MailerFormatterAbstract(ContentFormat format)
        {
            Format = format;
        }

        public IList<string> getContent(int key = -1)
        {
            switch (Format)
            {
                case ContentFormat.JSON:
                    return getJSONContent(key);
                case ContentFormat.XML:
                    return getXMLContent(key);
                case ContentFormat.TXT:
                    return getTXTContent(key);
            }

            return null;
        }

        public abstract IList<string> getJSONContent(int key = -1);

        public abstract IList<string> getTXTContent(int key = -1);

        public abstract IList<string> getXMLContent(int key = -1);

        public bool saveContent()
        {
            switch (Format)
            {
                case ContentFormat.JSON:
                    return saveJSONContent();
                case ContentFormat.XML:
                    return saveXMLContent();
                case ContentFormat.TXT:
                    return saveTXTContent();
            }

            return false;
        }

        public abstract bool saveJSONContent();

        public abstract bool saveTXTContent();

        public abstract bool saveXMLContent();
    }
}
