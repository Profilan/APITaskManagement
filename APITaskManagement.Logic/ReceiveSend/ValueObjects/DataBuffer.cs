
using APITaskManagement.Logic.Common;

namespace APITaskManagement.Logic.ReceiveSend.ValueObjects
{
    public class DataBuffer : ValueObject<DataBuffer>
    {
        public string Data { get; set; }

        protected DataBuffer() { }

        public DataBuffer(string data)
        {
            Data = data;
        }

        protected override bool EqualsCore(DataBuffer other)
        {
            return (Data == other.Data);
        }
    }
}
