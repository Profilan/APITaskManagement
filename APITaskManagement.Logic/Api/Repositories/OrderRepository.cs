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
    public class OrderRepository : IRepository<OrderHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderHeader GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderHeader> GetByIdentifier(OrderIdentifier orderIdentifier)
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                var query = from o in session.Query<OrderHeader>()
                            select o;

                query = query.Where(o => o.OrderIdentifier == orderIdentifier);

                return query.ToList();
            }
        }

        public void Insert(OrderHeader orderHeader)
        {
            using (ISession session = SessionFactory.GetNewSession("db2"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    orderHeader.SYSCREATED = DateTime.Now;
                    orderHeader.SYSMODIFIED = orderHeader.SYSCREATED;

                    session.Save(orderHeader);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<OrderHeader> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
