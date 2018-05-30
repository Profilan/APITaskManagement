using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Filer.Data
{
    public class Image
    {
        public virtual int Id { get; set; }
        public virtual string EANCode { get; set; }                // EAN
        public virtual string ImageUrl { get; set; }               // imagename

        public Image()
        {

        }
    }
}
