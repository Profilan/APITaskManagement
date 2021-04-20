using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring
{
    public class AvailabilityMonitor : Monitor
    {
        private readonly LogRepository _logRepository;

        public AvailabilityMonitor() : base()
        {
            _logRepository = new LogRepository();

        }

        public override void Run(ISet<Messenger> messengers)
        {
            var hostname = ConfigurationManager.AppSettings["DatabaseServer"];
            var portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["DatabasePort"]);
            try
            {
                var client = new TcpClient(hostname, portNumber);
            }
            catch (SocketException se)
            {
                foreach (var messenger in messengers)
                {
                    try
                    {
                        if (messenger.Enabled)
                        {
                            Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                            var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                            messengerToSend.Send("API Task Manager - Database is not available", "The database is not available. \nDetails:\n\n" + se.Message);
                        }
                    }
                    catch (Exception)
                    {


                    }

                }
            }

        }
    }
}
