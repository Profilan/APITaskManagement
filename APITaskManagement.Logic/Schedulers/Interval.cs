using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class Interval : ValueObject<Interval>
    {
        public int Amount { get; set; }
        public Unit Unit { get; set; }
        public int Seconds
        {
            get
            {
                int seconds = 0;

                switch (Unit)
                {
                    case Unit.Hours:
                        seconds = 3600 * Amount;
                        break;
                    case Unit.Minutes:
                        seconds = 60 * Amount;
                        break;
                    case Unit.Seconds:
                        seconds = Amount;
                        break;
                }

                return seconds;
            }
            set
            {
                Unit = Unit.Seconds;
                Amount = value;
            }
        }

        private Interval() { }

        public Interval(int amount, Unit unit)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount cannot be negative");
            }

            Amount = amount;
            Unit = unit;
        }

        protected override bool EqualsCore(Interval other)
        {
            return Amount == other.Amount
                && Unit == other.Unit;
        }
    }
}
