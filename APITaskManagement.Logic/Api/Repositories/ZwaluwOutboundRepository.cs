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
    public class ZwaluwOutboundRepository : IRepository<ZwaluwOutboundHeader, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ZwaluwOutboundHeader GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var item = session.Get<ZwaluwOutboundHeader>(id);
                if (item != null)
                {
                    NHibernateUtil.Initialize(item.Lines);
                }

                return item;
            }
        }

        public void Insert(ZwaluwOutboundHeader entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZwaluwOutboundHeader> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public ZwaluwOutboundHeader GetByIdAndDeliveryDate(int key, DateTime deliveryDate)
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var query = session.Query<ZwaluwOutboundHeader>();

                query = query.Where(x => x.SalesOrderHeaderId == key && x.DeliveryDate == deliveryDate);

                var items = query.ToList();

                if (items.Count > 0)
                {
                    var item = items[0];
                    NHibernateUtil.Initialize(item.Lines);

                    return (item);
                }
                else
                {
                    return null;
                }
             }
        }

        public IEnumerable<ZwaluwOutboundHeader> List()
        {
            using (ISession session = SessionFactory.GetNewSession("mvw"))
            {
                var query = session.Query<ZwaluwOutboundHeader>()
                    .FetchMany(x => x.Lines);

                return query.ToList();
            }
        }

        public void Update(ZwaluwOutboundHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
