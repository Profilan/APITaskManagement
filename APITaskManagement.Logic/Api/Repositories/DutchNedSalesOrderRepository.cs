using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class DutchNedSalesOrderRepository : IRepository<DutchNedSalesOrder, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DutchNedSalesOrder GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<DutchNedSalesOrder>(id);
                NHibernateUtil.Initialize(item.Lines);

                return item;
            }
        }

        public void Insert(DutchNedSalesOrder entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DutchNedSalesOrder> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DutchNedSalesOrder> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<DutchNedSalesOrder>()
                    .FetchMany(x => x.Lines);

                return query.ToList();
            }
        }

        public void Update(DutchNedSalesOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
