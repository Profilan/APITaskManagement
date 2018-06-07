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
        bool savePOSContent();

        IList<string> getContent(int key = -1);
        IList<string> getXMLContent(int key = -1);
        IList<string> getJSONContent(int key = -1);
        IList<string> getPOSContent(int key = -1);
    }
}
