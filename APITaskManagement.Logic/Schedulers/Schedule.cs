using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class Schedule : Entity<int>
    {
        public virtual string Title { get; protected set; }

        public virtual IList<Task> Tasks { get; protected set; }

        public Schedule()
        {
            Tasks = new List<Task>();


        }

        public virtual void AddTask(Task task)
        {
            Tasks.Add(task);
        }


        public virtual void ProcessAllTasks()
        {
            foreach (var task in Tasks)
            {
               
            }
        }
    }
}
