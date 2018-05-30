using APITaskManagement.Logic.Monitoring.Interfaces;
using APITaskManagement.Logic.Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskMonitor
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a valid Monitor Id");
                return 1;
            }
            var monitorRepository = new MonitorRepository();

            var monitor = monitorRepository.GetById(new Guid(args[0]));
            var monitorToRun = monitorRepository.GetByName(monitor.Name);
            

            try
            {
                monitorToRun.Run(monitor.Messengers);

                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
            
        }
    }
}
