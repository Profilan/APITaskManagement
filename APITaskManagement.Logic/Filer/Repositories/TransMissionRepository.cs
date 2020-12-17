using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Utils;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Repositories
{
    public class TransMissionRepository : IRepository<TransMissionHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TransMissionHeader GetById(int id)
        {

            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<TransMissionHeader>()
                    .Where(h => h.id == id)
                    .FetchMany(x => x.lines).ToList();

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

        public void Insert(TransMissionHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransMissionHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransMissionHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<TransMissionHeader>()
                    .FetchMany(x => x.lines);

                return query.ToList();
            }
        }

        public void Update(TransMissionHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
