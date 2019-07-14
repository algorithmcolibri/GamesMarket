using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesMarket.Models.DBModel
{
	public class Basket
	{
		public string UserId { get; set; }
		public int GameId { get; set; }
		public string GameName { get; set; }
		public double Price { get; set; }
	}
}