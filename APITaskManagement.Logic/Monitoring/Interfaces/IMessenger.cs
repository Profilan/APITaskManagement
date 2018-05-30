using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring.Interfaces
{
    public interface IMessenger
    {
        bool Enabled { get; set; }

        void Send(string subject, string body);
     }
}
