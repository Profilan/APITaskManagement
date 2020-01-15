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
    public class ZwaluwSalesOrderRepository : IRepository<ZwaluwSalesOrder, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ZwaluwSalesOrder GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<ZwaluwSalesOrder>(id);
                if (item != null)
                {
                    NHibernateUtil.Initialize(item.Lines);
                }

                return item;
            }
        }

        public void Insert(ZwaluwSalesOrder entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwSalesOrder> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwSalesOrder> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<ZwaluwSalesOrder>()
                    .FetchMany(x => x.Lines);

                return query.ToList();
            }
        }

        public void Update(ZwaluwSalesOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
