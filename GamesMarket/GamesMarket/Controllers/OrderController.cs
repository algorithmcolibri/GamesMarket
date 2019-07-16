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
    public class OrderController : Controller
    {
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            var Uid = User.Identity.GetUserId();

            var b = Repository.DBLogic.SelectBasket(Uid);

            double sum = Repository.DBLogic.returnSum(Uid);
            ViewBag.sum = sum;


            var cart = Repository.DBLogic.SelectBasket(Uid);


            return Redirect("Cart/Index");
        }

        [HttpPost]
        public ActionResult Index(string UserId)
        {
            var Uid = User.Identity.GetUserId();

            var b = Repository.DBLogic.SelectBasket(Uid);

            if (b.Count == 0)
            {
                //TempData["msg"] = "<script>alert('No items have been selected for purchase.');</script>";
                return Redirect("Home/Games");
            }

            if (!Repository.DBLogic.CreateOrder(b))
            {
                TempData["msg"] = "<script>alert('You don`t have enough money.');</script>";
                return Redirect("Cart/Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Successful purchase');</script>";
            }

            Repository.DBLogic.ClearBasket(Uid);

            double sum = Repository.DBLogic.returnSum(Uid);
            ViewBag.sum = sum;


            var cart = Repository.DBLogic.SelectBasket(Uid);

            return Redirect("Cart/Index");
        }


    }
}