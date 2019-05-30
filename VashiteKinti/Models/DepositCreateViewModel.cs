using System;
using System.Collections.Generic;
using System.ComponentModel;
using VashiteKinti.Data.Enums;
using VashiteKinti.Data.Models;
using VashiteKinti.Web.Models.Automapper;

namespace VashiteKinti.Web.Models
{
    public class DepositCreateViewModel : IMapFrom<Deposit>
    {
        [DisplayName("Име на банка")]
        public String BankName { get; set; }

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

        [DisplayName("Размер на депозита")]
        public int Size { get; set; }

        [DisplayName("Период на депозита")]
        public int Period { get; set; }

        [DisplayName("За кого е депозита")]
        public DepositHolder Holder { get; set; }

        [DisplayName("Вид лихва")]
        public InterestType InterestType { get; set; }

        [DisplayName("Довнасяне на суми")]
        public YesNoDoesntMatter ExtraMoneyPayIn { get; set; }

        [DisplayName("Възможност за овърдрафт")]
        public YesNoDoesntMatter OverdraftOpportunity { get; set; }

        [DisplayName("Възможност за кредит")]
        public YesNoDoesntMatter CreditOpportunity { get; set; }
    }
}
