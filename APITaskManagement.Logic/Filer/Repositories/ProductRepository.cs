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
    public class ProductRepository : IRepository<Product, string>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(string id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var product = session.Get<Product>(id);
                return product;
            }
        }

        public void Insert(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from p in session.Query<Product>()
                            select p;

                query = query.Where(p => p.EANCode != null && p.Condition == "A");

                return query.ToList();
            }
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
