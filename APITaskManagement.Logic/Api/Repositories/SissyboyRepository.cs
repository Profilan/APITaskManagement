using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class SissyboyRepository
    {
        public SissyboyDeliveryDate GetDeliveryDateById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<SissyboyDeliveryDate>(id);

                return item;
            }
        }

        public IEnumerable<SissyboyDeliveryDate> ListDeliveryDates()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<SissyboyDeliveryDate>();

                return query.ToList();
            }
        }
        public SissyboyReadyForPickup GetReadyForPickupById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<SissyboyReadyForPickup>(id);

                return item;
            }
        }

        public IEnumerable<SissyboyReadyForPickup> ListReadyForPickups()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<SissyboyReadyForPickup>();

                return query.ToList();
            }
        }
        public SissyboyTrackAndTrace GetTrackAndTraceById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var item = session.Get<SissyboyTrackAndTrace>(id);

                return item;
            }
        }

        public IEnumerable<SissyboyTrackAndTrace> ListTrackAndTraces()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<SissyboyTrackAndTrace>();

                return query.ToList();
            }
        }


    }
}
