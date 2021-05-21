
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;
using System.Collections.Generic;
using System.Linq;

namespace APITaskManagement.Logic.ReceiveSend.ValueObjects
{
    public class DataBuffer : ValueObject<DataBuffer>
    {
        public IList<Request> Data { get; set; }

        protected DataBuffer() { }

        public DataBuffer(IList<Request> data)
        {
            Data = data;
        }

        protected override bool EqualsCore(DataBuffer other)
        {
            return (Data.SequenceEqual(other.Data));
        }
    }
}
