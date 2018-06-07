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
    public class PosRepository : IRepository<Pos, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Pos GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var order = session.Get<Pos>(id);
                return order;
            }
        }

        public void Insert(Pos entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pos> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pos> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from p in session.Query<Pos>()
                            select p;

                return query.ToList();
            }
        }

        public void Update(Pos entity)
        {
            throw new NotImplementedException();
        }
    }
}
