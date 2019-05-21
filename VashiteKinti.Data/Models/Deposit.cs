using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VashiteKinti.Data.Enums;

namespace VashiteKinti.Data.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        [DisplayName("Банка")]
        public virtual Bank Bank { get; set; }
        public int BankId { get; set; }

        [DisplayName("Име на депозит")]
        public String Name { get; set; }

        [DisplayName("Минимална сума")]
        public double MinAmount { get; set; }
        [DisplayName("Лихва")]
        public double Interest { get; set; }

        [DisplayName("Изплащане на лихви")]
        public InterestPaymentMethod PaymentMethod { get; set; }

        [DisplayName("Валута")]
        public Currency Currency { get; set; }
    }
}