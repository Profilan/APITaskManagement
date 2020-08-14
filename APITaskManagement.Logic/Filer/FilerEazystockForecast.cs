using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer
{
    public class FilerEazystockForecast : FilerAbstract
    {
        public FilerEazystockForecast(IList<ContentFormat> formats) : base(formats)
        {

        }

        public override void SaveDocuments(Share share, Guid TaskId)
        {
            var dateNow = DateTime.Now.ToString("yyyyMMddTHHmm");

            foreach (var format in Formats)
            {
                var response = new Response();
                var UNC = share.UNCPath  + "_" + dateNow;
                // var UNC = share.UNCPath;

                var formatter = new EazystockForecastFormatter(format);

                if (formatter.saveContent(UNC))
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
