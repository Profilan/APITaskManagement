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
    public class FonQOfferFeedDSRepository : IRepository<FonQOfferFeedDS, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public FonQOfferFeedDS GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(FonQOfferFeedDS entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FonQOfferFeedDS> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FonQOfferFeedDS> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<FonQOfferFeedDS>();

                return query.ToList();
            }
        }

        public void Update(FonQOfferFeedDS entity)
        {
            throw new NotImplementedException();
        }
    }
}
