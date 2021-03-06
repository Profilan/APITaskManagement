﻿using ApiTaskManagement.Logic.Models;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Logging;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using APITaskManagement.Logic.Schedulers.Interfaces;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APITaskManagement.Logic.Schedulers
{
    public class Task : Entity<Guid>, ITask
    {
        [Required]
        public virtual string Title { get; set; }
        public virtual int ScheduleId { get; set; }

        public virtual Status Status { get; protected set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Authentication Authentication { get; set; }

        public virtual Url Url { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string QueueName { get; set; }
        public virtual HttpMethod HttpMethod { get; set; }
        public virtual TaskType TaskType { get; set; }

        public virtual string ContentFormats { get; set; }
        public virtual string Classname { get; set; }
        public virtual string SPLogger { get; set; }

        public virtual string LastRunResult { get; protected set; }
        public virtual DateTime LastRunTime { get; protected set; }
        public virtual string LastRunDetails { get; protected set; }

        public virtual string MailSender { get; set; }
        public virtual string MailRecipient { get; set; }

        public virtual int MaxErrors { get; set; }
        public virtual bool Active { get; set; }
        public virtual int TotalProcessedItems { get; set; }

        public virtual ISet<Share> Shares { get; set; }

        public virtual ISet<HttpHeader> HttpHeaders { get; set; }

        #region More Properties

        // Not persisted
        public virtual Response LatestResponse { get; set; }

        #endregion

        public Task()
        {
            Shares = new HashSet<Share>();
            HttpHeaders = new HashSet<HttpHeader>();
        }

        public Task(string title,
            int scheduleId,
            Schedule schedule,
            Authentication authentication,
            bool enabled,
            int totalProcessedItems = 100
            ) : base(Guid.NewGuid())
        {
            Active = false;

            Title = title;
            ScheduleId = scheduleId;
            Schedule = schedule;
            Authentication = authentication;
            Enabled = enabled;
            LastRunTime = DateTime.Now;
            LastRunResult = "0 Not Run";
            MaxErrors = 4;
            TotalProcessedItems = totalProcessedItems;

            Shares = new HashSet<Share>();
            HttpHeaders = new HashSet<HttpHeader>();
        }

        public virtual void Start()
        {
            Active = true;

            var taskStartedEvent = new TaskStartedEvent(this);
            DomainEvents.Raise(taskStartedEvent);

            Run();

            if (LatestResponse != null)
            {
                LastRunResult = LatestResponse.Code + " " + LatestResponse.Description;
                LastRunTime = DateTime.Now;
                LastRunDetails = LatestResponse.Detail;
            }

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);

            Active = false;

        }

        public virtual void Test(int ticks)
        {
            Active = true;

            var taskStartedEvent = new TaskStartedEvent(this);
            DomainEvents.Raise(taskStartedEvent);

            System.Threading.Thread.Sleep(ticks);

            var taskFinishedEvent = new TaskFinishedEvent(this);
            DomainEvents.Raise(taskFinishedEvent);

            Active = false;
        }

        public virtual void Stop()
        {
            Active = false;
        }

        public virtual bool IsActive()
        {
            return Active;
        }

        public virtual void ChangeLastRun(string lastRunResult, DateTime lastRunTime, string lastRunDetails)
        {
            LastRunResult = lastRunResult;
            LastRunTime = lastRunTime;
            LastRunDetails = lastRunDetails;
        }

        public virtual void EnableTask()
        {
            Enabled = true;
        }

        public virtual void DisableTask()
        {
            Enabled = false;
        }

        public virtual void Run()
        {

        }
        public virtual void AddHeader(HttpHeader header)
        {
            header.Task = this;
            HttpHeaders.Add(header);
        }
    }
}
