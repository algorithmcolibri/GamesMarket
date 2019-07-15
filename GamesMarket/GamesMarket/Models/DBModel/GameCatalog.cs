using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.DBModel
{
    public class GameCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeGame { get; set; }
        public decimal Price { get; set; }
        public string DescribeGame { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public byte[] Photo { get; set; }
    }
}