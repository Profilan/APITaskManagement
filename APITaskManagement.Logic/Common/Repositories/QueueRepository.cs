using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APITaskManagement.Logic.Common.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;

namespace APITaskManagement.Logic.Common.Repositories
{
    public class QueueRepository : IRepository<Queue, int>
    {
        public void Delete(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Load<Queue>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Queue GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<Queue>(id);
                return item;
            }
        }

        public void Insert(Queue entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Queue> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Queue> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Queue>()
                            select l;

                query = query.OrderByDescending(l => l.Id);

                return query.ToList();
            }
        }

        public IEnumerable<Queue> ListByTask(Guid taskId, int count)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Queue>()
                            select l;

                query = query.Where(l => l.Task.Id == taskId)
                    .Where(l => l.TryCount <= l.Task.MaxErrors).Take(count);

                return query.ToList();
            }
        }

        public IEnumerable<Queue> ListTasksBeforeDate(Guid taskId, DateTime sysCreated)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Queue>()
                            select l;

                query = query.Where(l => l.Task.Id == taskId)
                    .Where(l => l.SysCreated <= sysCreated);

                return query.ToList();
            }
        }

        public void Update(Queue entity)
        {
            throw new NotImplementedException();
        }
    }
}
