using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer
{
    class POSFormatter : FilerFormatterAbstract
    {
        public POSFormatter(ContentFormat format) : base(format)
        {

        }

        public override bool saveJSONContent()
        {
            throw new NotImplementedException();
        }

        public override bool saveXMLContent()
        {
            throw new NotImplementedException();
        }

        public override bool savePOSContent()
        {
            
        }
    }
}
