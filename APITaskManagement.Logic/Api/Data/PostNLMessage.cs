using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLMessage : ValueObject<PostNLMessage>
    {
        public string MessageID { get; set; }
        public string MessageTimeStamp { get; set; }
        public string Printertype { get; set; }

        public PostNLMessage(string messageID,
            string messageTimeStamp,
            string printerType)
        {
            MessageID = messageID;
            MessageTimeStamp = messageTimeStamp;
            Printertype = printerType;
        }

        protected override bool EqualsCore(PostNLMessage other)
        {
            return (MessageID == other.MessageID);
        }
    }
}
