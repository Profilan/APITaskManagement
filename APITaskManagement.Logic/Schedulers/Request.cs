using APITaskManagement.Logic.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APITaskManagement.Logic.Schedulers
{
    public class Request : Entity<int>
    {
        
        public virtual string Body { get; protected set; }
        public Response Response { get; protected set; }
        public int ReferenceId { get; set; }
        public bool ExecPost { get; protected set; }
        public bool ExecBefore { get; set; }
        public bool RequestAcknowledgment { get; protected set; }
        public IList<(string Field, string Value)> Params { get; set; }

        public Request(int id, 
            int referenceId,
            string body,
            bool execPost = false,
            bool requestAcknowledgement = false,
            bool execBefore = false)
            : base(id)
        {
            ReferenceId = referenceId;
            Body = body;
            ExecPost = execPost;
            ExecBefore = execBefore;
            RequestAcknowledgment = requestAcknowledgement;
            Params = new List<(string Field, string Value)>();
        }

        public Request(bool execPost = false)
        {
            ExecPost = execPost;
        }

        public void SetResponse(Response response)
        {
            Response = response;

        }

        private Request()
        {

        }
    }
}
