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
    public class UserRepository : IRepository<User, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var user = session.Get<User>(id);
                return user;
            }
        }

        public void Insert(User entity)
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

        public IEnumerable<User> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<User>().ToList();
            }
        }

        public IEnumerable<User> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<User>().ToList();
            }
        }

        public void Update(User entity)
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
