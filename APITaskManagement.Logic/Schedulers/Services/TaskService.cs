using APITaskManagement.Logic.Utils;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.Schedulers.Services
{
    public class TaskService
    {
        public static IList<Task> GetAllTasks()
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                return session.Query<Task>().ToList();
            }
        }

        public static void Add(Task task)
        {
            using (ISession session = SessionFactory.GetNewSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(task);
                    transaction.Commit();
                }
            }
        }
    }
}
