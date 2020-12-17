using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Api;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace APITaskManagement.Web.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        public string Url { get; set; }

        public HttpMethod HttpMethod { get; set; }

        [Required]
        public TaskType TaskType { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string OAuthUrl { get; set; }

        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }


        [Display(Name = "Http Headers")]
        public IEnumerable<HttpHeader> Headers { get; set; }


        [Required]
        public int Amount { get; set; }

        [Required]
        public Unit Unit { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public DateTime ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }
        public string ScheduleDays { get; set; }
        public int ScheduleRecurrence { get; set; }

        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public int Queued { get; set; }

        [Display(Name = "Last Run Time")]
        public DateTime LastRunTime { get; set; }

        [Display(Name = "Last Run Details")]
        public string LastRunDetails { get; set; }

        [Display(Name = "Last Run Result")]
        public string LastRunResult { get; set; }

        [Display(Name = "Url")]
        public int UrlId { get; set; }
        public IEnumerable<Url> Urls { get; set; }

        [Display(Name = "Maximum Errors")]
        public int MaxErrors { get; set; }

        [Display(Name = "Total processed items")]
        public int TotalProcessedItems { get; set; }

        // FTP fields

        [Display(Name = "Sync Method")]
        public FtpSyncMethod FtpSyncMethod { get; set; }

        [Display(Name = "Server address")]
        public string FTPServerAddress { get; set; }

        [Display(Name = "Connection Type")]
        public FtpConnectionType FtpConnectionType { get; set; }

        [Display(Name = "Port")]
        public int FtpPort { get; set; }

        [Display(Name = "Local Folder")]
        public string FtpLocalFolder { get; set; }

        [Display(Name = "Remote Folder")]
        public string FtpRemoteFolder { get; set; }

        // Disk fields

        [Display(Name = "Classname")]
        public string Classname { get; set; }

        [Display(Name = "Stored Procedure Logger")]
        public string SPLogger { get; set; }

        [Display(Name = "Formats")]
        public IEnumerable<SelectListItem> Formats { get; set; }
        public int[] SelectedFormats { get; set; }

        [Display(Name = "Shares")]
        public virtual IEnumerable<Share> Shares { get; set; }
        public ISet<Share> SelectedShares { get; set; }

        // Mail fields

        [Display(Name = "Mail From")]
        public string MailSender { get; set; }

        [Display(Name = "Mail To")]
        public string MailRecipient { get; set; }
    }
}