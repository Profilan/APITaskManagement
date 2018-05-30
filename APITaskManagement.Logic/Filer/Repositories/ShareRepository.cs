using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Repositories
{
    public class ShareRepository : IRepository<Share, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Share GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var share = session.Get<Share>(id);
                return share;
            }
        }

        public void Insert(Share entity)
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

        public IEnumerable<Share> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Share> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from p in session.Query<Share>()
                            select p;

                return query.ToList();
            }
        }

        public void Update(Share entity)
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
