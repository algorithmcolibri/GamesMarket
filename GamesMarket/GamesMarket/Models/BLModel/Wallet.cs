using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.BLModel
{
    public class Wallet
    {
        public string IdUser { get; set; }
        [Display(Name = "Balance")]
        [RegularExpression(@"^\d+$", ErrorMessage = "UPRN must be numeric")]
        public decimal Balance { get; set; }
        public string Bank { get; set; }
    }
}