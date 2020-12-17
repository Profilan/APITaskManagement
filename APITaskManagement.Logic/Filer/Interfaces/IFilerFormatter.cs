using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Interfaces
{
    public interface IFilerFormatter
    {
        ContentFormat Format { get; set; }

        bool saveContent(string destination);
        bool saveXMLContent();
        bool saveJSONContent();
        bool saveTXTContent();

        IList<string> getContent(int key = -1);
        IList<string> GetXMLContent(int key = -1);
        IList<string> getJSONContent(int key = -1);
        IList<string> getTXTContent(int key = -1);
    }
}
