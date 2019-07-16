using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamesMarket.Models.BLModel;

namespace GamesMarket.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Wallets()
        {
            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);
            Wallet wall = null;
            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
                wall = item;
            }

            return View(wall);
        }

        [HttpPost]
        public ActionResult Wallets(Wallet model)
        {
            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);


            Repository.DBLogic.AddMoneyOnWallet(id, Convert.ToDouble(model.Balance));

            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);
            Wallet wall = null;
            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
                wall = item;
            }

            return View(wall);
        }

        public ActionResult Library(int ID = 0, string SearchGame = null)
        {
            ViewBag.TypeGame = BusinessLogic.BusinessLogic.SelectTypeGame();

            IList<Models.BLModel.GameCatalog> gc;
            if (!string.IsNullOrEmpty(SearchGame))
            {
                gc = Repository.DBLogic.SelectGameFind(SearchGame);
            }
            else
            {
                gc = Repository.DBLogic.SelectGamePriceJanr(ID);
            }

            var id = User.Identity.GetUserId();

            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);
            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            ViewBag.UGame = Repository.DBLogic.SelectGameByUser(id);

            return View();
        }
    }
}