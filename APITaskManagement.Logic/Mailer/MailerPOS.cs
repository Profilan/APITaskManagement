using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace APITaskManagement.Logic.Mailer
{
    public class MailerPOS : MailerAbstract
    {
        private readonly QueueRepository _queueRepository;

        public MailerPOS(IList<ContentFormat> formats) : base(formats)
        {
            _queueRepository = new QueueRepository();
        }

        public override IList<Request> Prepare(Task task)
        {
            var dateNow = DateTime.Now;
            var items = _queueRepository.ListByTask(task.Id, 100);
            var requests = new List<Request>();

            if (items.Count() > 0)
            {
                int id = 0;
                foreach (var format in Formats)
                {
                    var formatter = new POSFormatter(format);

                    var lines = new List<string>();
                    lines.Add("DDA:" + dateNow.ToString("ddMMyyyy"));
                    lines.Add("DTI:" + dateNow.ToString("HHmm"));
                    lines.Add("DTO:" + items.Count());

                    var nbg = 1;
                    var ids = new List<int>();
                    var keys = new List<int>();
                    foreach (var item in items)
                    {
                        var response = new Response();

                        var result = formatter.getContent(item.Key);

                        lines.Add("");
                        lines.Add("NBG:" + nbg);
                        lines.Add("NNT:Order");
                        lines.AddRange(result);
                        lines.Add("FEN:" + nbg++);

                        ids.Add(item.Id);
                        keys.Add(item.Key);

                    }

                    var contentFormat = Enum.GetName(typeof(ContentFormat), format);
                    var path = Path.GetTempPath() + @"\EEK_ORDER." + contentFormat;

                    var body = new RequestBody()
                    {
                        Subject = "De Eekhoorn Order",
                        Body = "De inhoud van dit bericht is afkomstig van De Eekhoorn Woodworkings B.V",
                        Ids = ids,
                        Keys = keys,
                        Attachment = lines,
                        Path = path
                    };

                    id++;
                    var request = new Request(id, id, JsonConvert.SerializeObject(body));

                    requests.Add(request);
                }
            }

            return requests;
        }
    }
}
