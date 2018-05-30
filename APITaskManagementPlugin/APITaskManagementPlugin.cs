using System;
using MM.Monitor.Client;

namespace APITaskManagementPlugin
{
    public class APITaskManagementPlugin : ClientPlugin
    {
        private const int COMMAND_ERRORS_DETECTED = 1;
        private const int COMMAND_INACTIVITY_DETECTED = 2;
        private const int COMMAND_UNAVAILABILITY_DETECTED = 3;

        private bool ErrorsDetected;
        private bool InactivityDetected;
        private bool UnavailabilityDetected;

        public override void PluginLoaded()
        {
            ErrorsDetected = false;
            InactivityDetected = false;
            UnavailabilityDetected = false;
        }

        public override string GetPluginName()
        {
            return "API Task Monitor Plugin";
        }

        public override string GetPluginDescription()
        {
            return "This plugin sends alarms from the API Task Management System";
        }

        public override Groups GetAdditionalComputerDetails()
        {
            Groups container = new Groups();
            Group mainGroup = new Group("API Task Monitor Plugin");
            SimpleItem message = new SimpleItem("Errors detected in API Task Management");
            SimpleItem date = new SimpleItem("Current Date:  ", System.DateTime.Now.Date.ToShortDateString());
            mainGroup.Items.Add(date);
            mainGroup.Items.Add(message);
            container.Add(mainGroup);

            return container;
        }

        public override void CommandReceived(int commandId)
        {
            switch (commandId)
            {
                case COMMAND_ERRORS_DETECTED:
                    ErrorsDetected = true;
                    break;
                case COMMAND_INACTIVITY_DETECTED:
                    InactivityDetected = true;
                    break;
                case COMMAND_UNAVAILABILITY_DETECTED:
                    UnavailabilityDetected = true;
                    break;
            }
        }

        public override void PluginDataCheck()
        {
            if (ErrorsDetected)
            {
                SendNotificationToAllDevices("Error(s) detected in API Task Management", NotificationPriority.CRITICAL);
                ErrorsDetected = false;
            }

            if (InactivityDetected)
            {
                SendNotificationToAllDevices("API is not called for a long time", NotificationPriority.ELEVATED);
                InactivityDetected = false;
            }

            if (UnavailabilityDetected)
            {
                SendNotificationToAllDevices("API Task Manager - Database is not available", NotificationPriority.CRITICAL);
                UnavailabilityDetected = false;
            }
        }
    }
}
