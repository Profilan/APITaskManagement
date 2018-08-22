using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLGroup : ValueObject<PostNLGroup>
    {
        public string GroupCount { get; set; }
        public string GroupSequence { get; set; }
        public string GroupType { get; set; }
        public string MainBarcode { get; set; }

        public PostNLGroup(string groupCount,
            string groupSequence,
            string groupType,
            string mainBarcode)
        {
            GroupCount = groupCount;
            GroupSequence = groupSequence;
            GroupType = groupType;
            MainBarcode = mainBarcode;
        }

        protected override bool EqualsCore(PostNLGroup other)
        {
            return (GroupType == other.GroupType &&  MainBarcode == other.MainBarcode);
        }
    }
}
