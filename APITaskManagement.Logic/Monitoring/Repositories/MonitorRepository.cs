using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Monitoring.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring.Repositories
{
    public class MonitorRepository : IRepository<Monitor, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Monitor GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var monitor = session.Get<Monitor>(id);
                return monitor;
            }
        }

        public IMonitor GetByName(string name)
        {
            Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + name);

            return (IMonitor)Activator.CreateInstance(t);
        }

        public void Insert(Monitor entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Monitor> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Monitor> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<Monitor>().ToList();
            }
        }

        public void Update(Monitor entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }
    }
}
