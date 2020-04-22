using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class ZwaluwSalesOrderRepository : IRepository<ZwaluwSalesOrderHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ZwaluwSalesOrderHeader GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<ZwaluwSalesOrderHeader>(id);
                if (item != null)
                {
                    NHibernateUtil.Initialize(item.Lines);
                }

                return item;
            }
        }

        public void Insert(ZwaluwSalesOrderHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwSalesOrderHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwSalesOrderHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<ZwaluwSalesOrderHeader>()
                    .FetchMany(x => x.Lines);

                return query.ToList();
            }
        }

        public void Update(ZwaluwSalesOrderHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
