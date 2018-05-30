using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Management
{

    public partial class User : Entity<int>
    {
        /**
         * @var string
         */
        public virtual string Username { get; set; }
    
        /**
         * @var string
         */
        public virtual string DisplayName { get; set; }

        /**
         * @var string
         */
        public virtual string Email { get; set; }
    
        /**
         * @var string
         */
        public virtual string Password { get; set; }
    
        /**
         * @var string
         */
        public virtual string Apikey { get; set; }
    
        /**
         * @var bool
         */
        public virtual bool Enabled { get; set; }
    
        /**
         * @var integer
         */
        public virtual int State { get; set; }
    
        /**
         * @var \DateTime
         */
        public virtual DateTime SysCreated { get; set; }
    
        /**
         * @var integer
         */
        public virtual int SysCreator { get; set; }
    
        /**
         * @var \DateTime
         */
        public virtual DateTime SysModified { get; set; }
    
        /**
         * @var integer
         */
        public virtual int SysModifier { get; set; }
    
        public virtual ISet<Role> Roles { get; set; }

        protected User()
        {
            
            Roles = new HashSet<Role>();
        }

        public User(string username,
            string password) : this()
        {
            Username = username;
            Password = password;
        }
    }

}
