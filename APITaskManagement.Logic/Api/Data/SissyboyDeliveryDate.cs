﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class SissyboyDeliveryDate
    {
        public virtual int Id { get; set; }
        public virtual string PoNumber { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
