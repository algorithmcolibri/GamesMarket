using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.DBModel
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Bank { get; set; }

    }
}