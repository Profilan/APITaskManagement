using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Interfaces
{
    public interface IApiRepository
    {
        IEnumerable<IApi> List();

        IApi GetByName(string name);
    }
}
