using APITaskManagement.Logic.Common;
using APITaskManagement.Service.DependencyResolution;
using APITaskManagement.Service.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            IContainer container = IoC.Initialize();
            DomainEvents.Container = container;
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));

            if (Environment.UserInteractive)
            {
                APITaskScheduler service1 = new APITaskScheduler();
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new APITaskScheduler()
                };
                ServiceBase.Run(ServicesToRun);
            }
            
        }
    }
}
