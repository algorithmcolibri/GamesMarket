using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.BLModel
{
	public class Order
	{
		public int IdOrder { get; set; }
		public int GameId { get; set; }
		public string IdUser { get; set; }
		public double TotalPrice { get; set; }
	}
}