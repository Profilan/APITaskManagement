using APITaskManagement.Logic.Common;
using System;
using System.Text;

namespace APITaskManagement.Logic.Schedulers
{
    public class Request : Entity<int>
    {
        
        public virtual string Body { get; protected set; }
        public Response Response { get; protected set; }
        public int ReferenceId { get; protected set; }
        public bool ExecPost { get; protected set; }

        public Request(int id, 
            int referenceId,
            string body,
            bool execPost = false)
            : base(id)
        {
            ReferenceId = referenceId;
            Body = body;
            ExecPost = execPost;
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
