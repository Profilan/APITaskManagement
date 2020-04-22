using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class HttpHeader
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public virtual Task Task { get; set; }

        public HttpHeader() { }

        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
