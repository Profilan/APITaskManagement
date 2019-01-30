using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Api.Data;
using System.Collections.Generic;
using NHibernate;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class DutchNedInsertedOrderlinestatusRepository : IRepository<DutchNedInsertedOrderlinestatus, int>
    {
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public DutchNedInsertedOrderlinestatus GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(DutchNedInsertedOrderlinestatus entity)
        {
            using (ISession session = Utils.SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<DutchNedInsertedOrderlinestatus> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DutchNedInsertedOrderlinestatus> List()
        {
            throw new System.NotImplementedException();
        }

        public void Update(DutchNedInsertedOrderlinestatus entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
