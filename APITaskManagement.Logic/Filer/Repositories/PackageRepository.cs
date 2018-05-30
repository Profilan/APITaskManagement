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
    public class PackageRepository : IRepository<Package, string>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Package GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Package entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> ListByProductCode(string productCode)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from i in session.Query<Package>()
                            select i;

                query = query.Where(i => i.ProductCode == productCode);

                return query.ToList();
            }
        }
        public void Update(Package entity)
        {
            throw new NotImplementedException();
        }
    }
}
