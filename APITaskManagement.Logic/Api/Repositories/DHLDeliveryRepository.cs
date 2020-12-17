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
    public class DHLDeliveryRepository : IRepository<DHLDeliveryHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DHLDeliveryHeader GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<DHLDeliveryHeader>()
                    .Where(h => h.Id == id)
                    .FetchMany(x => x.DeliveryLines)
                    .ThenFetchMany(x => x.Barcodes).ToList();

                if (query.Count > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public void Insert(DHLDeliveryHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DHLDeliveryHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DHLDeliveryHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<DHLDeliveryHeader>()
                    .FetchMany(x => x.DeliveryLines)
                        .ThenFetchMany(x => x.Barcodes);

                return query.ToList();
            }
        }

        public void Update(DHLDeliveryHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
