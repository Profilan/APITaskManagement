using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring
{
    class UrlMonitor : Monitor
    {
        protected readonly UrlRepository _urlRepository;

        public UrlMonitor() : base()
        {
            _urlRepository = new UrlRepository();

        }

        public override void Run(ISet<Messenger> messengers)
        {
            try
            {
                var urls = _urlRepository.List();

                foreach (var url in urls)
                {
                    if (!String.IsNullOrEmpty(url.ExternalUrl))
                    {
                        if (!HostIsReachable(url.ExternalUrl))
                        {
                            foreach (var messenger in messengers)
                            {
                                try
                                {
                                    if (messenger.Enabled)
                                    {
                                        Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                                        var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                                        messengerToSend.Send("API Task Manager - Url is not available", "The url is not available. \nDetails:\n\n" + url.ExternalUrl);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch 
            {
            }
        }

        private bool HostIsReachable(string url)
        {
            IPHostEntry host;
            Uri uri = new Uri(url);

            try
            {
                host = Dns.GetHostEntry(uri.Host);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
