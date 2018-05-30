using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

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

        public virtual ISet<Url> Urls { get; set; }


    }
}