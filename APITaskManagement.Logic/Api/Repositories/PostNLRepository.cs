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
    public class PostNLRepository : IRepository<PostNLHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PostNLHeader GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<PostNLHeader>()
                    .Where(h => h.Id == id).ToList();

                return query.First();
            }
        }

        public void Insert(PostNLHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostNLHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostNLHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<PostNLHeader>();

                return query.ToList();
            }
        }

        public IEnumerable<PostNLLine> ListLinesById(string mainBarcode)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<PostNLLine>()
                    .Where(h => h.MainBarcode == mainBarcode);

                return query.ToList();
            }
        }

        public void Update(PostNLHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
