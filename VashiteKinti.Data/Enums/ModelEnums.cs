using System;
using System.Collections.Generic;
using System.Text;

namespace VashiteKinti.Data.Enums
{
    public enum InterestPaymentMethod
    {
        AT_MATURITY = 0, //На падеж, нямам идея от превода
        IN_ADVANCE,
        PER_MONTH,
        ON_PERIOD_END
    }

    public enum Currency
    {
        BGN = 0,
        EUR,
        USD,
        GBP,
        JPY
    }

    public enum CardTypes
    {
        CREDIT = 0,
        DEBIT
    }

    public enum InvestmentType
    {
        MUTUAL_FUNDS = 0,
        PENSION_FUNDS,
        MASS_PRIVATIZATION,
        ALTERNATIVE
    }

    public enum CreditType
    {
        CREDIT = 0,
        DEBIT
    }

    public enum InsuranceTypes
    {
        PROPERTY = 0,
        HEALTH,
        LIFE,
        TRAVEL
    }
}
