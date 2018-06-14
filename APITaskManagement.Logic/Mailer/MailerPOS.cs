using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
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

        public override void Prepare(Task task)
        {
            var dateNow = DateTime.Now;
            var items = _queueRepository.ListByTask(task.Id, 100);

            foreach (var format in Formats)
            {
                var formatter = new POSFormatter(format);
                var contentFormat = Enum.GetName(typeof(ContentFormat), format);
                var UNC = Path.GetTempPath() + @"\EEK_ORDER." + contentFormat;
                //var UNC = Path.GetTempFileName();

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
                        response.Detail = "POS order " + item.Key + " was sent succesfully";
                    }
                    catch (Exception)
                    {
                        response.Code = 400;
                        response.Description = "Bad Request";
                        response.Detail = "There was an error when sending POS order " + item.Key;
                    }

                    Responses.Add(response);
                }

                var array = new string[lines.Count];
                lines.CopyTo(array, 0);

                System.IO.File.WriteAllLines(UNC, lines);

                var subject = "De Eekhoorn Order";
                var body = "De inhoud van dit bericht is afkomstig van De Eekhoorn Woodworkings B.V";

                SendMail(task.MailSender, task.MailRecipient, subject, body, UNC);
                
            }

        }
    }
}
