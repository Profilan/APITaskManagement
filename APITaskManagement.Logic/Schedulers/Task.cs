using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Logging;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APITaskManagement.Logic.Schedulers
{
    public class Task : Entity<Guid>
    {
        [Required]
        public virtual string Title { get; set; }
        public virtual int ScheduleId { get; protected set; }

        public virtual Status Status { get; protected set; }
        public virtual Interval Interval { get; set; }
        public virtual Authentication Authentication { get; set; }

        public virtual Url Url { get; set; }
        public virtual bool Enabled { get; protected set; }
        public virtual string QueueName { get; set; }
        public virtual HttpMethod HttpMethod { get; set; }
        public virtual TaskType TaskType { get; set; }

        public virtual string ContentFormats { get; set; }
        public virtual string Classname { get; set; }
        public virtual string SPLogger { get; set; }

        public virtual string LastRunResult { get; protected set; }
        public virtual DateTime LastRunTime { get; protected set; }
        public virtual string LastRunDetails { get; protected set; }

        public virtual int MaxErrors { get; set; }
        public virtual bool Active { get; set; }

        public virtual ISet<Share> Shares { get; set; }

        #region More Properties

        // Not persisted


        #endregion

        public Task()
        {
            Shares = new HashSet<Share>();
        }

        public Task(string title,
            int scheduleId,
            Interval interval,
            Authentication authentication,
            bool enabled
            ) : base(Guid.NewGuid())
        {
            Active = false;

            Title = title;
            ScheduleId = scheduleId;
            Interval = interval;
            Authentication = authentication;
            Enabled = enabled;
            LastRunTime = DateTime.Now;
            LastRunResult = "0 Not Run";
            MaxErrors = 4;

            Shares = new HashSet<Share>();
        }

        public void Start()
        {
            Active = true;

            Response latestResponse = null;
            switch (TaskType)
            {
                case TaskType.API:
                    
                    Type t = Type.GetType("APITaskManagement.Logic.Api." + Classname);
                    var queue = (IApi)Activator.CreateInstance(t, Classname);
                    queue.AddLogger(new ApplicationLogger());
                    queue.AddLogger(new SystemLogger());
                    queue.SendRequestsToTarget(HttpMethod, Url, Authentication, this);
                    latestResponse = queue.GetLatestResponse();
                    break;
                case TaskType.FTP:

                    break;
                case TaskType.FILE:
                    
                    var formats = ContentFormats.Split(';');
                    List<ContentFormat> contentFormats = new List<ContentFormat>();
                    foreach (var format in formats)
                    {
                        contentFormats.Add((ContentFormat)Enum.Parse(typeof(ContentFormat), format));
                    }
                    t = Type.GetType("APITaskManagement.Logic.Filer." + Classname);
                    var filer = (IFiler)Activator.CreateInstance(t, contentFormats);
                    filer.AddLogger(new SystemLogger());
                    filer.Send(Shares, this.Id);
                    latestResponse = filer.GetLatestResponse();
                    break;
                default:
                    break;
            }
            if (latestResponse != null)
            {
                LastRunResult = latestResponse.Code + " " + latestResponse.Description;
                LastRunTime = DateTime.Now;
                LastRunDetails = latestResponse.Detail;
            }

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);

            Active = false;

        }

        private void OnFinish()
        {
            
        }

        public virtual void Stop()
        {
            Active = false;
        }

        public bool IsActive()
        {
            return Active;
        }

        public void ChangeLastRun(string lastRunResult, DateTime lastRunTime, string lastRunDetails)
        {
            LastRunResult = lastRunResult;
            LastRunTime = lastRunTime;
            LastRunDetails = lastRunDetails;
        }

        public void EnableTask()
        {
            Enabled = true;
        }

        public void DisableTask()
        {
            Enabled = false;
        }
    }
}
