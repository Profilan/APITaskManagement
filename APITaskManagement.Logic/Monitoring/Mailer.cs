using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace APITaskManagement.Logic.Monitoring
{
    public class Mailer : Messenger
    {
        public Mailer() : base()
        {
            Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["MailEnabled"]);
        }

        public override void Send(string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();

            var username = ConfigurationManager.AppSettings["MailUsername"];
            var password = ConfigurationManager.AppSettings["MailPassword"];
            var mailtTo = ConfigurationManager.AppSettings["MailTo"];
            var mailFrom = ConfigurationManager.AppSettings["MailFrom"];

            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
            client.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["MailUseDefaultCredentials"]);
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["MailEnableSsl"]);
            client.Host = ConfigurationManager.AppSettings["MailHost"];
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mm = new MailMessage(mailFrom, mailtTo, subject, body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

        }
    }
}
