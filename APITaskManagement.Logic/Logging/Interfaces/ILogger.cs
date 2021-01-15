
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Logging.Interfaces
{
    public interface ILogger
    {
        void Log(Request request, Url url, User user, Task task);
        void Log(Response response, Share share, User user, Task task);
        void Log(Response response, string recipient, User user, Task task);
    }
}
