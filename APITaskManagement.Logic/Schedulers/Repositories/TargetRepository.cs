using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.Schedulers.Repositories
{
    public class TargetRepository : IRepository<Target, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Target GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var target = session.Get<Target>(id);
                return target;
            }
        }

        public void Insert(Target entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Target> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Target> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<Target>().ToList();
            }
        }

        public void Update(Target entity)
        {
            throw new NotImplementedException();
        }
    }
}
