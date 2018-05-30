using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class ScheduleInstance
    {
        private const int ScheduleId = 1;

        public static Schedule Instance { get; private set; }

        public static void Init()
        {
            Instance = new Schedule();
        }
    }

}
