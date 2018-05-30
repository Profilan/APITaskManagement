using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Monitoring;
using APITaskManagement.Logic.Monitoring.ApplicationEvents;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace APITaskMonitor.Service
{
    public partial class APITaskMonitor : ServiceBase
    {

        private IList<IMonitor> _monitors { get; set; }

        public APITaskMonitor()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("API Task Monitor"))
            {
                System.Diagnostics.EventLog.CreateEventSource("API Task Monitor", "API Task Management");
            }
            eventLog1.Source = "API Task Monitor";
            eventLog1.Log = "API Task Management";

            _monitors = new List<IMonitor>();
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("API Task Monitor started", System.Diagnostics.EventLogEntryType.Information, 0);

            // Create messengers
            var messengers = new List<IMessenger>();
            messengers.Add(new Mailer());
            messengers.Add(new Pulseway());

            // Add monitors
            _monitors.Add(new EventsMonitor());
            _monitors.Add(new InactivityMonitor());
            _monitors.Add(new AvailabilityMonitor());

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000 * Convert.ToInt32(ConfigurationManager.AppSettings["MonitorInterval"]);
            timer.Elapsed += (sender, e) => this.OnTimer(sender, e);
            timer.Start();

            
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            eventLog1.WriteEntry("Execute monitoring", System.Diagnostics.EventLogEntryType.Information, 1001);

            DomainEvents.Register<ErrorDetectedEvent>(OnErrorDetected);

            foreach (var monitor in _monitors)
            {
                monitor.Run();
            }
        }

        private void OnErrorDetected(ErrorDetectedEvent obj)
        {

            eventLog1.WriteEntry("Error detected", System.Diagnostics.EventLogEntryType.Error, 1002);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("API Task Scheduler stopped", System.Diagnostics.EventLogEntryType.Information, 0);
        }
    }
}
