using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLDimension : ValueObject<PostNLDimension>
    {
        public string Weight { get; set; }
        public string Volume { get; set; }

        public PostNLDimension(string weight,
            string volume)
        {
            Weight = weight;
            Volume = volume;
        }

        protected override bool EqualsCore(PostNLDimension other)
        {
            return (Weight == other.Weight && Volume == other.Volume);
        }
    }
}
