using GamesMarket.Models.BLModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamesMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var gc = BusinessLogic.BusinessLogic.SelectGameCatalog();

            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            return View(gc);
        }

        [HttpPost]
        public ActionResult Index(GameCatalog gameCatalog)
        {
            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            var gc = BusinessLogic.BusinessLogic.SelectGameCatalog();

            Repository.DBLogic.AddGameToBasket(id, gameCatalog.Id, Convert.ToDouble(gameCatalog.Price));

            return Redirect("Home/Index");
        }

        public ActionResult Games(int ID = 0, string SearchGame = null, int From = 0, int To = 100000)
        {
            ViewBag.TypeGame = BusinessLogic.BusinessLogic.SelectTypeGame();

            IList<Models.BLModel.GameCatalog> gc;
            if (!string.IsNullOrEmpty(SearchGame))
            {
                gc = Repository.DBLogic.SelectGameFind(SearchGame);
            }
            else { 
                if (ID == 0)
                {
                    //gc = BusinessLogic.BusinessLogic.SelectGameCatalog();
                    gc = Repository.DBLogic.SelectGamePriceJanr(From, To, ID);
                }
                else
                {
                   gc = Repository.DBLogic.SelectGamePriceJanr(From, To, ID);
                }
            }
            if (gc.Count == 0)
            {
                ViewBag.mes = "Nothing was found";
            }

            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }
            

            return View(gc);
        }

        [HttpPost]
        public ActionResult Games(GameCatalog gameCatalog)
        {
            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            var gc = BusinessLogic.BusinessLogic.SelectGameCatalog();

            Repository.DBLogic.AddGameToBasket(id, gameCatalog.Id, Convert.ToDouble(gameCatalog.Price));

            return Redirect("Games");
        }

        public ActionResult About()
        {
            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);

            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);

            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}