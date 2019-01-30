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
        event EventHandler<ElapsedEventArgs> Timer;
        
        void Start();
        void Stop();
    }
}
