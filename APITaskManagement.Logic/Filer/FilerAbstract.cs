﻿using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer
{
    public class Document : ValueObject<Document>
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public ContentFormat ContentFormat { get; set; }
        public string UNC { get; set; }

        protected override bool EqualsCore(Document other)
        {
            return other.Code == Code && other.ContentFormat == ContentFormat;
        }
    }

    public abstract class FilerAbstract : IFiler
    {
        public IList<ContentFormat> Formats { get; set; }
        public IList<Response> Responses { get; set; }
        protected IList<ILogger> Loggers { get; set; }


        public FilerAbstract(IList<ContentFormat> formats)
        {
            Formats = formats;
            Responses = new List<Response>();
            Loggers = new List<ILogger>();
        }

        public abstract void SaveDocuments(Share share, Schedulers.Task Task);

        public void Send(ISet<Share> shares, Schedulers.Task task)
        {
            foreach (var share in shares)
            {
                SaveDocuments(share, task);

                foreach (var response in Responses)
                {
                    LogResponse(response, share, task.SPLogger);
                }
            }
        }

        public void AddLogger(ILogger logger)
        {
            Loggers.Add(logger);
        }

        protected void LogResponse(Response response, Share share, string spLogger)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(response, share, spLogger);
            }
        }

        public Response GetLatestResponse()
        {
            if (Responses.Count > 0)
            {
                return Responses.Last();
            }

            return null;
        }
    }
}
