using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Utils;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class ZwaluwInboundRepository : IRepository<ZwaluwInboundHeader, Guid>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ZwaluwInboundHeader GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var item = session.Get<ZwaluwInboundHeader>(id);
                if (item != null)
                {
                    NHibernateUtil.Initialize(item.Lines);
                }

                return item;
            }
        }

        public void Insert(ZwaluwInboundHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwInboundHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwInboundHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var query = session.Query<ZwaluwInboundHeader>()
                    .FetchMany(x => x.Lines);

                return query.ToList();
            }
        }

        public void Update(ZwaluwInboundHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
