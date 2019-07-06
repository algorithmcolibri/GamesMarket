using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.BLModel
{
    public class GameCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeGame { get; set; }
        public string NameJanr { get; set; }
        public decimal Price { get; set; }
        public string DescribeGame { get; set; }
    }
}