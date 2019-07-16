using GamesMarket.Models.BLModel;
using GamesMarket.Models.DBModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamesMarket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {

            var id = User.Identity.GetUserId();

            var cart = Repository.DBLogic.SelectBasket(id);

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            };

            return View(cart);
        }

        [HttpPost]
        public ActionResult Index(int id, int idGames, bool allDelete)
        {
            var Uid = User.Identity.GetUserId();

            var cart = Repository.DBLogic.SelectBasket(Uid);


            if (allDelete == false)
            {
                Repository.DBLogic.DeleteGameFromoBasket(Uid, idGames, id);
            }
            else
            {
                Repository.DBLogic.ClearBasket(Uid);
                //foreach (var item in cart)
                //{
                //    Repository.DBLogic.DeleteGameFromoBasket(Uid, item.GameId, item.Id);
                //}
            }

            ViewBag.sum = Repository.DBLogic.returnSum(Uid);

            var bal = BusinessLogic.BusinessLogic.SelectWallet(Uid);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            return Redirect("Cart/Index");
        }

        
    }
}