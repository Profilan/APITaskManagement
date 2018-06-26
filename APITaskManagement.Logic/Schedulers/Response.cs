using APITaskManagement.Logic.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class Response : ValueObject<Response>
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public int Id { get; set; }

        public Response() { }

        public Response(int code,
            string description,
            string detail)
        {
            Code = code;
            Description = description;
            Detail = detail;
        }

        protected override bool EqualsCore(Response other)
        {
            return Code == other.Code
                && Description == other.Description
                && Detail == other.Detail;
        }
    }
}
