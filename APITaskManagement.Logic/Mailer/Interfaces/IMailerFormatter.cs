using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Mailer.Interfaces
{
    public interface IMailerFormatter
    {
        ContentFormat Format { get; set; }

        bool saveContent();
        bool saveXMLContent();
        bool saveJSONContent();
        bool saveTXTContent();

        IList<string> getContent(int key = -1);
        IList<string> getXMLContent(int key = -1);
        IList<string> getJSONContent(int key = -1);
        IList<string> getTXTContent(int key = -1);
    }
}
