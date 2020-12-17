using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTaskManagement.Logic.Models
{
    public class Schedule : ValueObject<Schedule>
    {
        public virtual ScheduleType Type { get; set; }
        public virtual string Days { get; set; }
        public virtual int Recurrence { get; set; }
        public virtual Interval Interval { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime? End { get; set; }

        private Schedule() { }

        public Schedule(ScheduleType type,
            DateTime start,
            DateTime? end,
            string days,
            int recurrence,
            Interval interval)
        {
            Type = type;
            Start = start;
            End = end;
            Days = days;
            Recurrence = recurrence;
            Interval = interval;
        }

        protected override bool EqualsCore(Schedule other)
        {
            return (Type == other.Type && Days == other.Days && Recurrence == other.Recurrence && Interval == other.Interval);
        }
    }
}
