using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
    }
}