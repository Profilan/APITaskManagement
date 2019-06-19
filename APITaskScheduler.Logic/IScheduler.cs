using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace APITaskScheduler.Logic
{
    public interface IScheduler
    {
        void Start();
        void Stop();
        void DisableTask(string taskId);
        void EnableTask(string taskId);
        void Run(Guid taskId);
    }
}
