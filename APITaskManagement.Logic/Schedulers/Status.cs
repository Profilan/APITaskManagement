using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers 
{
    public class Status : ValueObject<Status>
    {
        public int Code { get; set; }
        public string Description { get; set; }

        private Status() { }

        public Status(int code, string description)
        {
            Code = code;
            Description = description;
        }

        protected override bool EqualsCore(Status other)
        {
            return Code == other.Code
                && Description == other.Description;
        }
    }
}
