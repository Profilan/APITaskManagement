﻿using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Interfaces
{
    public interface IFiler
    {
        IList<ContentFormat> Formats { get; set; }
        IList<Response> Responses { get; set; }

        void SaveDocuments(Share share, Schedulers.Task task);
        void Send(ISet<Share> shares, Schedulers.Task task);
        void AddLogger(ILogger logger);
        Response GetLatestResponse();
    }
}
