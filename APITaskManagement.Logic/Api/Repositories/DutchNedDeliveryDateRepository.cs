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
    public class DutchNedDeliveryDateRepository : IRepository<DutchNedDeliveryDate, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DutchNedDeliveryDate GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(DutchNedDeliveryDate entity)
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

        public IEnumerable<DutchNedDeliveryDate> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DutchNedDeliveryDate> List()
        {
            throw new NotImplementedException();
        }

        public void Update(DutchNedDeliveryDate entity)
        {
            throw new NotImplementedException();
        }
    }
}
