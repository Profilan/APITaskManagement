using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers.Interfaces
{
    public interface ITask
    {
        void Start();
        void Run();
    }
}
