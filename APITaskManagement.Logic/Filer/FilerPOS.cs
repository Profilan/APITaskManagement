using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;

namespace APITaskManagement.Logic.Filer
{
    public class FilerPOS : FilerAbstract
    {
        private readonly QueueRepository _queueRepository;

        public FilerPOS(IList<ContentFormat> formats) : base(formats)
        {
            _queueRepository = new QueueRepository();
        }

        public override void SaveDocuments(Share share, Guid taskId)
        {
            var dateNow = DateTime.Now;
            var items = _queueRepository.ListByTask(taskId, 100);

            foreach (var format in Formats)
            {
                var formatter = new POSFormatter(format);
                var UNC = share.UNCPath + "." + Enum.GetName(typeof(ContentFormat), format);

                var lines = new List<string>();
                lines.Add("DDA:" + dateNow.ToString("ddMMyyyy"));
                lines.Add("DTI:" + dateNow.ToString("HHmm"));
                lines.Add("DTO:" + items.Count());

                var nbg = 1;
                foreach (var item in items)
                {
                    var response = new Response();
                    
                    try
                    {
                        var result = formatter.getContent(item.Key);

                        lines.Add("");
                        lines.Add("NBG:" + nbg);
                        lines.Add("NNT:Order");
                        lines.AddRange(result);
                        lines.Add("FEN:" + nbg++);

                        response.Code = 201;
                        response.Description = "Created";
                        response.Detail = UNC + " was saved succesfully";
                    }
                    catch (Exception)
                    {
                        response.Code = 400;
                        response.Description = "Bad Request";
                        response.Detail = "There was an error when saving " + UNC;
                    }

                    Responses.Add(response);
                }

                var array = new string[lines.Count];
                lines.CopyTo(array, 0);

                System.IO.File.WriteAllLines(UNC, lines);
            }
            
        }
    }
}
