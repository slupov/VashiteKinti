using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VashiteKinti.Data.Models;
using VashiteKinti.Web.Models.Automapper;

namespace VashiteKinti.Web.Models
{
    public class DepositEditViewModel : IMapFrom<Deposit>
    {
        //TODO Add similar fields
        public List<Deposit> Deposits { get; set; }

        [DisplayName("Валути")]
        public List<SelectListItem> Currencies { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "BGN", Text = "BGN" },
        new SelectListItem { Value = "EUR", Text = "EUR" },
        new SelectListItem { Value = "USD", Text = "USD"  },
        new SelectListItem { Value = "GBP", Text = "GBP"  },
        new SelectListItem { Value = "CHF", Text = "CHF"  },
    };

        [DisplayName("Изплащане на лихви")]

        public List<SelectListItem> Interests { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "AT_MATURITY", Text = "На падеж" },
        new SelectListItem { Value = "IN_ADVANCE", Text = "Авансово" },
        new SelectListItem { Value = "PER_MONTH", Text = "Ежемесечно"  },
        new SelectListItem { Value = "ON_PERIOD_END", Text = "На край на период"  },
    };

        public string CurrencyId { get; set; }
        public string InterestId { get; set; }

        [DisplayName("Размер на депозита")]
        public int DepositSize { get; set; }

        [DisplayName("Период на депозита")]
        public List<SelectListItem> DepositPeriod { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1 месец" },
            new SelectListItem { Value = "3", Text = "3 месеца" },
            new SelectListItem { Value = "6", Text = "6 месеца"  },
            new SelectListItem { Value = "9", Text = "9 месеца"  },
            new SelectListItem { Value = "12", Text = "12 месеца"  },
            new SelectListItem { Value = "18", Text = "18 месеца"  },
            new SelectListItem { Value = "24", Text = "24 месеца"  },
            new SelectListItem { Value = "36", Text = "36 месеца"  },
            new SelectListItem { Value = "48", Text = "48 месеца"  },
            new SelectListItem { Value = "60", Text = "60 месеца"  },
        };
        public string DepositPeriodId { get; set; } //PARSE TO INT!

        [DisplayName("За кого е депозита")]
        public List<SelectListItem> DepositHolder { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DOESNT_MATTER", Text = "Без значение" },
            new SelectListItem { Value = "INDIVIDUAL", Text = "Физическо лице" },
            new SelectListItem { Value = "RETIRED", Text = "Пенсионер" },
            new SelectListItem { Value = "CHILD", Text = "Дете"  },
        };
        public string DepositHolderId { get; set; }

        [DisplayName("Вид лихва")]
        public List<SelectListItem> InterestType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DOESNT_MATTER", Text = "Без значение" },
            new SelectListItem { Value = "FIXED", Text = "Фиксирана" },
            new SelectListItem { Value = "VARIABLE", Text = "Променлива" },
        };
        public string InterestTypeId { get; set; }

        [DisplayName("Довнасяне на суми")]
        public List<SelectListItem> ExtraMoneyPayIn { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DOESNT_MATTER", Text = "Без значение" },
            new SelectListItem { Value = "YES", Text = "Да" },
            new SelectListItem { Value = "NO", Text = "Не" },
        };
        public string ExtraMoneyPayInId { get; set; }

        [DisplayName("Възможност за овърдрафт")]
        public List<SelectListItem> OverdraftOpportunity { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DOESNT_MATTER", Text = "Без значение" },
            new SelectListItem { Value = "YES", Text = "Да" },
            new SelectListItem { Value = "NO", Text = "Не" },
        };
        public string OverdraftOpportunityId { get; set; }

        [DisplayName("Възможност за кредит")]
        public List<SelectListItem> CreditOpportunity { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DOESNT_MATTER", Text = "Без значение" },
            new SelectListItem { Value = "YES", Text = "Да" },
            new SelectListItem { Value = "NO", Text = "Не" },
        };
        public string CreditOpportunityId { get; set; }
    }
}
