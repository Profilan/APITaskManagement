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
    public class FonQOfferFeedB2BRepository : IRepository<FonQOfferFeedB2B, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public FonQOfferFeedB2B GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(FonQOfferFeedB2B entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FonQOfferFeedB2B> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FonQOfferFeedB2B> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<FonQOfferFeedB2B>();

                return query.ToList();
            }
        }

        public void Update(FonQOfferFeedB2B entity)
        {
            throw new NotImplementedException();
        }
    }
}
