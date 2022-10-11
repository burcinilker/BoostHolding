using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public enum Unit
    {
        [Display(Name = "₺ (Turkish Lira)", Description = "Turkish Lira")]
        TurkishLira = 0,

        [Display(Name = "$ (US Dollar)", Description = "US Dollar")]
        USDollar = 1,

        [Display(Name = "€ (Euro)", Description = "European Euro")]
        EUEuro = 2,

        [Display(Name = "£ (British Pound)", Description = "British Pound")]
        BritishPound = 3,
    }
}
