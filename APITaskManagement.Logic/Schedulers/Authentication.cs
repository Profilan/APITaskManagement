using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class Authentication : ValueObject<Authentication>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string OAuthUrl { get; set; }
        public string OAuthAudience { get; set; }
        public string ApiKey { get; set; }

        protected Authentication()
        {

        }

        public Authentication(string username,
            string password,
            AuthenticationType authenticationType,
            string scope = null,
            string grantType = null,
            string oAuthUrl = null,
            string oAuthAudience = null) : this()
        {
            Username = username;
            Password = password;
            AuthenticationType = authenticationType;
            Scope = scope;
            GrantType = grantType;
            OAuthUrl = oAuthUrl;
            OAuthAudience = oAuthAudience;
        }

        protected override bool EqualsCore(Authentication other)
        {
            return Username  == other.Username
                && Password == other.Password
                && AuthenticationType == other.AuthenticationType;
        }
    }
}
