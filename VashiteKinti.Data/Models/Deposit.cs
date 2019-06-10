using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VashiteKinti.Data.Enums;

namespace VashiteKinti.Data.Models
{
    public class Deposit
    {
        [Key]
        [Index("IX_CompositeUniqueKey", 1, IsUnique = true)]
        public int Id { get; set; }

        [DisplayName("Банка")]
        public virtual Bank Bank { get; set; }

        [Index("IX_CompositeUniqueKey", 2, IsUnique = true)]
        public int BankId { get; set; }

        [DisplayName("Име на депозит")]
        [Index("IX_CompositeUniqueKey", 3, IsUnique = true)]
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