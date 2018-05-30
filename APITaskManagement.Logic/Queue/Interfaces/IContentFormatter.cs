using System.Collections.Generic;
using System.Net.Http;

namespace APITaskManagement.Logic.Queue.Interfaces
{
    public interface IContentFormatter
    {
        string GetJsonContent(int key, IDictionary<string, string> properties);
    }
}
