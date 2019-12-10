using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Monitoring
{
    public class InactivityMonitor : Monitor
    {
        private readonly LogRepository _logRepository;
        private readonly UrlRepository _urlRepository;
        private readonly ShareRepository _shareRepository;

        public InactivityMonitor() : base()
        {
            _logRepository = new LogRepository();
            _urlRepository = new UrlRepository();
            _shareRepository = new ShareRepository();
        }

        public override void Run(ISet<Messenger> messengers)
        {
            try
            {
                foreach (var url in _urlRepository.List())
                {
                    if (!String.IsNullOrEmpty(url.Address) && url.MonitorInactivity == true)
                    {
                        var inactivityTimeout = url.InactivityTimeout;
                        var log = _logRepository.GetLatestByUrl(url.Address);
                        var timespan = new TimeSpan(0, 0, inactivityTimeout.Seconds);
                        var dateNow = DateTime.Now;

                        if (log.TimeStamp.Add(timespan) < dateNow)
                        {
                            // Overdue
                            foreach (var messenger in messengers)
                            {
                                try
                                {
                                    if (messenger.Enabled)
                                    {
                                        Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                                        var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                                        messengerToSend.Send("API is not called for a long time", "The url " + url.Address + " has not been called for " + timespan.ToString());
                                    }
                                }
                                catch (Exception)
                                {

                                    
                                }
                               
                            }
                        }
                    }
                }
              
                foreach (var share in _shareRepository.List())
                {
                    if (!String.IsNullOrEmpty(share.UNCPath) && share.MonitorInactivity == true)
                    {
                        var inactivityTimeout = share.InactivityTimeout;
                        var log = _logRepository.GetLatestByUrl(share.UNCPath);
                        var timespan = new TimeSpan(0, 0, inactivityTimeout.Seconds);
                        var dateNow = DateTime.Now;

                        if (log.TimeStamp.Add(timespan) < dateNow)
                        {
                            // Overdue
                            foreach (var messenger in messengers)
                            {
                                try
                                {
                                    if (messenger.Enabled)
                                    {
                                        Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                                        var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                                        messengerToSend.Send("API is not called for a long time", "The share " + share.Name + " has not been called for " + timespan.ToString());
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
            catch (Exception)
            {

                
            }
            
        }
    }
}
