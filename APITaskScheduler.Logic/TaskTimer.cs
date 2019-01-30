using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskScheduler.Logic
{
    public class TaskTimer : System.Timers.Timer
    {
        public Guid Id { get; private set; }

        public TaskTimer(Guid id) : base()
        {
            Id = id;
        }
    }
}
