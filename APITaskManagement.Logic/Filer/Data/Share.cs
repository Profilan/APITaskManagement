using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class Share : Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual string UNCPath { get; set; }

        public virtual ISet<Task> Tasks { get; set; }

        public Share()
        {
            Tasks = new HashSet<Task>();
        }
    }
}
