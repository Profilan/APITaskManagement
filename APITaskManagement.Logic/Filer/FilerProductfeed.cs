using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer
{
    public class FilerProductfeed : FilerAbstract
    {
        public FilerProductfeed(IList<ContentFormat> formats) : base(formats)
        {
            
        }

        public override void SaveDocuments(Share share)
        {
            foreach (var format in Formats)
            {
               var response = new Response();
               var UNC = share.UNCPath + "." + Enum.GetName(typeof(ContentFormat), format);

                var formatter = new ProductfeedFormatter(format);

                if (formatter.saveContent(share.UNCPath))
                {
                    response.Code = 201;
                    response.Description = "Created";
                    response.Detail = UNC + " was saved succesfully";
                }
                else
                {
                    response.Code = 400;
                    response.Description = "Bad Request";
                    response.Detail = "There was an error when saving " + UNC;
                }

                Responses.Add(response);
            }
        }
    }
}
