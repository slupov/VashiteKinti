using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VashiteKinti.Data.Models
{
    public class Bank
    {
        public int Id { get; set; }

        [DisplayName("Име на банка")]
        public String Name { get; set; }

        public virtual IList<Deposit> Deposits { get; set; }

        public virtual IList<Card> Cards { get; set; }
        public virtual IList<Credit> Credits { get; set; }

        public virtual IList<Insurance> Insurances { get; set; }
        public virtual IList<Investment> Investments { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
