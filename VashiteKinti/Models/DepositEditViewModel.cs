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

    }
}
