using System.Collections.Generic;
using System.Linq;
using APITaskManagement.Logic.Utils;
using APITaskManagement.Logic.Schedulers.Data;
using NHibernate;
using System;
using APITaskManagement.Logic.Common.Interfaces;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;
using NHibernate.Linq;

namespace APITaskManagement.Logic.Schedulers.Repositories
{
    public class TaskRepository : ITaskRepository, IRepository<Task, Guid>
    {
        public IEnumerable<Task> List(string sortOrder, string searchString, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                var task = session.Get<Task>(id);
                if (task != null)
                {
                    NHibernateUtil.Initialize(task.HttpHeaders);
                }
                return task;
            }
        }

        public void Insert(Task entity)
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

        public void Update(Task entity)
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

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> List()
        {
            
            using (ISession session = SessionFactory.GetNewSession())
            {
                var query = session.Query<Task>()
                    .FetchMany(x => x.HttpHeaders)
                    .OrderBy(x => x.Title);

                return query.ToList();
               
            }
        }
    }
}
