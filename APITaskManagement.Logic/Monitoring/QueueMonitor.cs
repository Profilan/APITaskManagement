using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Monitoring.Interfaces;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring
{
    public class QueueMonitor : Monitor
    {
        private readonly QueueRepository _queueRepository;
        private readonly TaskRepository _taskRepository;

        public QueueMonitor() : base()
        {
            _taskRepository = new TaskRepository();
            _queueRepository = new QueueRepository();
        }

        public override void Run(ISet<Messenger> messengers)
        {
            try
            {
                foreach (var task in _taskRepository.List())
                {
                    if (task.Url != null)
                    {
                        var inactivityTimeout = task.Url.InactivityTimeout;
                        var timespan = new TimeSpan(0, 0, inactivityTimeout.Seconds);
                        var dateNow = DateTime.Now;
                        var sysCreated = dateNow.Subtract(timespan);
                        var queueItems = _queueRepository.ListTasksBeforeDate(task.Id, sysCreated);

                        if (queueItems.Count() > 0)
                        {
                            foreach (var messenger in messengers)
                            {
                                try
                                {
                                    Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + messenger.Name);

                                    var messengerToSend = (IMessenger)Activator.CreateInstance(t);
                                    messengerToSend.Send("API Queue contains overdue items", "The task '" + task.Title + "' contains " + queueItems.Count() + " overdue item(s) before " + sysCreated.ToString());
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


    }
}
