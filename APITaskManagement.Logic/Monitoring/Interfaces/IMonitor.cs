using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring.Interfaces
{
    public interface IMonitor
    {
        bool Enabled { get; set; }

        void Run(ISet<Messenger> messengers);
    }
}
