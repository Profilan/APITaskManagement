using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLContact : ValueObject<PostNLContact>
    {
        public string ContactType { get; set; }
        public string Email { get; set; }
        public string TelNr { get; set; }

        public PostNLContact(string contactType,
            string email,
            string telNr)
        {
            ContactType = contactType;
            Email = email;
            TelNr = telNr;
        }

        protected override bool EqualsCore(PostNLContact other)
        {
            return (Email == other.Email && TelNr == other.TelNr);
        }
    }
}
