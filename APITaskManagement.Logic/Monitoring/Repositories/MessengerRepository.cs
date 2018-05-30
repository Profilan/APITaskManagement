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
    public class MessengerRepository : IRepository<Messenger, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Messenger GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var messenger = session.Get<Messenger>(id);
                return messenger;
            }
        }

        public IMessenger GetByName(string name)
        {
            Type t = Type.GetType("APITaskManagement.Logic.Monitoring." + name);

            return (IMessenger)Activator.CreateInstance(t);
        }

        public void Insert(Messenger entity)
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

        public IEnumerable<Messenger> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Messenger> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<Messenger>().ToList();
            }
        }

        public void Update(Messenger entity)
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
