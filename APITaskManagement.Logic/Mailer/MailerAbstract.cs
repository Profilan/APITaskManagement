using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Mailer.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace APITaskManagement.Logic.Mailer
{
    public class RequestBody
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<int> Ids { get; set; }
        public IList<int> Keys { get; set; }
        public IList<string> Attachment { get; set; }
        public string Path { get; set; }
    }

    public abstract class MailerAbstract : IMailer
    {
        public IList<ContentFormat> Formats { get; set; }
        public IList<Request> Requests { get; set; }
        protected IList<ILogger> Loggers { get; set; }
        protected SmtpClient client { get; set; }

        private readonly UserRepository userRepository = new UserRepository();
        protected User user;

        public MailerAbstract(IList<ContentFormat> formats)
        {
            Formats = formats;
            Requests = new List<Request>();
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

            user = userRepository.GetById(40); // Administrator
        }

        public void AddLogger(ILogger logger)
        {
            Loggers.Add(logger);
        }

        public Response GetLatestResponse()
        {
            if (Requests.Count > 0)
            {
                return Requests.Last().Response;
            }

            return null;
        }

        public abstract IList<Request> Prepare(Task task);

        public void Send(Task task)
        {
            Requests = Prepare(task);

            foreach (var request in Requests)
            {
                // LogResponse(response, task.MailRecipient, task.SPLogger);

                var requestBody = JsonConvert.DeserializeObject<RequestBody>(request.Body);

                try
                {
                    System.IO.File.WriteAllLines(requestBody.Path, requestBody.Attachment);
                    SendMail(task.MailSender, task.MailRecipient, requestBody.Subject, requestBody.Body, requestBody.Path);

                    var response = new Response(201, "Created", "Mail was sent succesfully");
                    request.SetResponse(response);

                    LogResponse(response, task.MailRecipient, requestBody, task);
                }
                catch (Exception e)
                {
                    var response = new Response(400, "Bad Request", "Mail was not sent succesfully: " + e.Message);

                    LogResponse(response, task.MailRecipient, requestBody, task);
                }
             }
        }


        protected void LogResponse(Response response, string recipient, RequestBody body, Task task)
        {
            foreach (ILogger logger in Loggers)
            {
                var originalDetail = response.Detail;
                foreach (var id in body.Ids)
                {
                    response.Id = id;
                    response.Detail = originalDetail + " with id: [" + id + "]";
                    logger.Log(response, recipient, user, task);
                }
                
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
