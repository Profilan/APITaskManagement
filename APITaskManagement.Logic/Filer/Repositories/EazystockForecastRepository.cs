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
    public class EazystockForecastRepository : IRepository<EazystockForecast, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public EazystockForecast GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(EazystockForecast entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EazystockForecast> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EazystockForecast> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from p in session.Query<EazystockForecast>()
                            select p;

                return query.ToList();
            }
        }

        public void Update(EazystockForecast entity)
        {
            throw new NotImplementedException();
        }
    }
}
