using System;
using System.Collections.Generic;
using System.Text;
using VashiteKinti.Data.Enums;

namespace VashiteKinti.Data.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        public virtual Bank Bank { get; set; }
        public int BankId { get; set; }

        public String Name { get; set; }

        public double MinAmount { get; set; }
        public double Interest { get; set; }

        public InterestPaymentMethod PaymentMethod { get; set; }    
        public Currency Currency { get; set; }
    }
}