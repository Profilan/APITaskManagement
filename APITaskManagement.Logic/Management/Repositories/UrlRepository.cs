using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Management.Repositories
{
    public class UrlRepository : IRepository<Url, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Url GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var url = session.Get<Url>(id);
                return url;
            }
        }

        public void Insert(Url entity)
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

        public IEnumerable<Url> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Url> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<Url>().ToList();
            }
        }

        public void Update(Url entity)
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
