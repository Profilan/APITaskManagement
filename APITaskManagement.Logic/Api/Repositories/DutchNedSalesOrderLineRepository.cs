using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Helpers;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class DutchNedSalesOrderLineRepository : IRepository<DutchNedSalesOrderLine, int>
    {
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public DutchNedSalesOrderLine GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(DutchNedSalesOrderLine entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DutchNedSalesOrderLine> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DutchNedSalesOrderLine> List()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DutchNedSalesOrderLine> GetLinesBySalesOrderHeaderId(int salesOrderHeaderId)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                IQuery query = (IQuery)session.GetNamedQuery("GetLinesBySalesOrderHeaderId");
                query.SetInt32("SalesOrderHeaderId", salesOrderHeaderId);

                var items = query.List<DutchNedSalesOrderLine>();

                if (items.Count > 0)
                    return items;
                else
                    return null;
            }
        }

        public void Update(DutchNedSalesOrderLine entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
