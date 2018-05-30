using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APITaskManagement.Logic.Common.Interfaces
{
    public interface IRepository<TEntity, TId>
    {
        IEnumerable<TEntity> List(string sortOrder, string searchString, int pageSize, int pageNumber);
        IEnumerable<TEntity> List();
        TEntity GetById(TId id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TId id);
    }
}
