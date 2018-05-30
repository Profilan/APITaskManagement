using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Queue.Repositories
{
    public class QueueTableItemRepository : IRepository<QueueTableItem, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public QueueTableItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(QueueTableItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QueueTableItem> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QueueTableItem> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<QueueTableItem>().ToList();
            }
        }

        public IEnumerable<QueueTableItem> ListByTask(Guid taskId, int count)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<QueueTableItem>()
                            select l;

                query = query.Where(l => l.Task.Id == taskId)
                    .Where(l => l.TryCount <= l.Task.MaxErrors).Take(count);

                return query.ToList();
            }
        }

        public void Update(QueueTableItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
