using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Monitoring.ApplicationEvents;
using APITaskManagement.Logic.Monitoring.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.Monitoring
{
    public class EventsMonitor : Monitor
    {
        private readonly LogRepository _logRepository;

        public EventsMonitor() : base()
        {
            _logRepository = new LogRepository();
            
        }

        public override void Run(ISet<Messenger> messengers)
        {
            DateTime start, end;

            var dateNow = DateTime.Now.AddMonths(-1);
            start = DateTime.Now.AddMonths(-1).Date;
            end = DateTime.Now.AddDays(1).Date;

            try
            {
                // var logs = _logRepository.List(start, end, ErrorType.ERR);
                var logs = APITaskMonitor.Services.LogService.ListByUrlAndStatus(start, end, ErrorType.ERR);
                if (logs.Count() > 0)
                {
                    var errorDetectedEvent = new ErrorDetectedEvent(this);
                    foreach (var messenger in messengers)
                    {
                        try
                        {
                            if (messenger.Enabled)
                            {
                                Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                                var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                                messengerToSend.Send("Error(s) detected in API Manager", "There where " + logs.Count() + " error(s) detected.");
                            }
                        }
                        catch (Exception e)
                        {
                            var message = e.Message;
                            
                        }
                        
                    }

                    foreach (var log in logs)
                    {
                        log.Acknowledged = true;
                        _logRepository.Update(log);
                    }

                    DomainEvents.Raise(errorDetectedEvent);
                }
            }
            catch (Exception e)
            {

                var message = e.Message;
            }
            
        }
    }
}
