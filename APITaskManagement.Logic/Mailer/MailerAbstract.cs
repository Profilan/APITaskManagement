using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Mailer.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace APITaskManagement.Logic.Mailer
{
    public class Document : ValueObject<Document>
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public ContentFormat ContentFormat { get; set; }
        public string UNC { get; set; }

        protected override bool EqualsCore(Document other)
        {
            return other.Code == Code && other.ContentFormat == ContentFormat;
        }
    }

    public abstract class MailerAbstract : IMailer
    {
        public IList<ContentFormat> Formats { get; set; }
        public IList<Response> Responses { get; set; }
        protected IList<ILogger> Loggers { get; set; }
        protected SmtpClient client { get; set; }

        public MailerAbstract(IList<ContentFormat> formats)
        {
            Formats = formats;
            Responses = new List<Response>();
            Loggers = new List<ILogger>();

            var username = ConfigurationManager.AppSettings["MailUsername"];
            var password = ConfigurationManager.AppSettings["MailPassword"];

            client = new SmtpClient();
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
            client.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["MailUseDefaultCredentials"]);
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["MailEnableSsl"]);
            client.Host = ConfigurationManager.AppSettings["MailHost"];
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void AddLogger(ILogger logger)
        {
            Loggers.Add(logger);
        }

        public Response GetLatestResponse()
        {
            if (Responses.Count > 0)
            {
                return Responses.Last();
            }

            return null;
        }

        public abstract void Prepare(Task task);

        public void Send(Task task)
        {
            Prepare(task);

            foreach (var response in Responses)
            {
                LogResponse(response, task.MailRecipient);
            }
        }


        protected void LogResponse(Response response, string recipient)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(response, recipient);
            }
        }

        protected void SendMail(string mailFrom, string mailTo, string subject, string body, string attachment = null)
        {
            var addresses = mailTo.Split(';');

            MailMessage mm = new MailMessage();
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress(mailFrom);
            foreach (var address in addresses)
            {
                mm.To.Add(new MailAddress(address));
            }
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                if (attachment != null)
                {
                    Attachment data = new Attachment(attachment);
                    mm.Attachments.Add(data);
                    client.Send(mm);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
