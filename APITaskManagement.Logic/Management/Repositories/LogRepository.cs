using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace APITaskManagement.Logic.Management.Repositories
{
    public class LogRepository : IRepository<Log, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Log GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var log = session.Get<Log>(id);
                return log;
            }
        }

        public IEnumerable<Log> GetByDate(DateTime startDate, DateTime endDate)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                query = query.Where(l => l.TimeStamp >= startDate && l.TimeStamp <= endDate);
 
                return query.ToList();
            }
        }

        public Log GetLatestByUrl(string url)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                query = query.Where(l => l.Url == url)
                    .OrderByDescending(l => l.Id).Take(1);
                

                return query.ToList().Last();
            }
        }

        public void Insert(Log entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Log> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(l => l.Url.Contains(searchString)
                                           || l.Message.Contains(searchString)
                                           || l.Detail.Contains(searchString)
                                           || l.PriorityName.Contains(searchString)
                                           );
                }

                switch (sortOrder)
                {
                    case "timestamp_asc":
                        query = query.OrderBy(l => l.TimeStamp);
                        break;
                    case "priority":
                        query = query.OrderBy(l => l.PriorityName);
                        break;
                    case "timestamp_desc":
                    default:
                        query = query.OrderByDescending(l => l.TimeStamp);
                        break;
                }
                    
                return query.ToPagedList(pageNumber, pageSize);
            }
        }

        public IEnumerable<Log> List(string sortOrder, string searchString, int pageSize, int pageNumber, DateTime startDate, DateTime endDate)
        {

            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(l => l.Url.Contains(searchString)
                                           || l.Message.Contains(searchString)
                                           || l.Detail.Contains(searchString));
                }

                query = query.Where(l => l.TimeStamp >= startDate && l.TimeStamp <= endDate);

                switch (sortOrder)
                {
                    case "timestamp_asc":
                        query = query.OrderBy(l => l.TimeStamp);
                        break;
                    case "priority":
                        query = query.OrderBy(l => l.PriorityName);
                        break;
                    case "timestamp_desc":
                    default:
                        query = query.OrderByDescending(l => l.TimeStamp);
                        break;
                }

                return query.ToPagedList(pageNumber, pageSize);
            }
        }

        public IEnumerable<Log> List(DateTime startDate, DateTime endDate, ErrorType errorType, bool acknowledged = false)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                query = query.Where(l => l.TimeStamp >= startDate && l.TimeStamp <= endDate && l.Priority == Convert.ToInt32(errorType));
                query = query.Where(l => l.Acknowledged == acknowledged);

                return query.ToList();
            }
        }

        public IEnumerable<Log> List()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = from l in session.Query<Log>()
                            select l;

                return query.ToList();
            }
        }

        public void Update(Log entity)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }
    }
}
