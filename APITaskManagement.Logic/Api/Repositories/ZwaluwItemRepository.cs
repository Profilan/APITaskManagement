using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class ZwaluwItemRepository : IRepository<ZwaluwItem, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ZwaluwItem GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var item = session.Get<ZwaluwItem>(id);

                return item;
            }
        }

        public void Insert(ZwaluwItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwItem> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwItem> List()
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var query = session.Query<ZwaluwItem>();

                return query.ToList();
            }
        }

        public void Update(ZwaluwItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
