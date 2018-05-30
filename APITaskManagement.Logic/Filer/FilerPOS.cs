using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Common;

namespace APITaskManagement.Logic.Filer
{
    public class FilerPOS : FilerAbstract
    {
        public FilerPOS(IList<ContentFormat> formats) : base(formats)
        {

        }

        public override void SaveDocuments(Share share)
        {
            throw new NotImplementedException();
        }
    }
}
