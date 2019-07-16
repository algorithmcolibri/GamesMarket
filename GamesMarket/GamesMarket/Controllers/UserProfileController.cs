using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamesMarket.Models.BLModel;

namespace GamesMarket.Controllers
{
    [Authorize]
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
            var id = User.Identity.GetUserId();

            ViewBag.TypeGame = BusinessLogic.BusinessLogic.SelectTypeGame();

            IList<Models.BLModel.GameCatalog> ug;
            if (!string.IsNullOrEmpty(SearchGame))
            {
                ug = Repository.DBLogic.SelectGameFindByUser(SearchGame, id);
            }
            else
            {
                if (ID != 0)
                {
                    ug = Repository.DBLogic.SelectGameJanrByUser(ID, id);
                }
                else
                {
                    ug = Repository.DBLogic.SelectGameByUser(id);
                }
            }
            if (ug.Count == 0)
            {
                ViewBag.mes = "Nothing was found";
            }


            ViewBag.sum = Repository.DBLogic.returnSum(id);
            var bal = BusinessLogic.BusinessLogic.SelectWallet(id);
            foreach (var item in bal)
            {
                ViewBag.balanc = item.Balance;
            }

            //ViewBag.UGame = Repository.DBLogic.SelectGameByUser(id);

            return View(ug);
        }
    }
}