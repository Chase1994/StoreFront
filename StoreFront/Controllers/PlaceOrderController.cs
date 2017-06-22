using StoreFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFront.Controllers
{
    public class PlaceOrderController : Controller
    {
        StoreFront.Data.StoreFrontDataEntities dc = new StoreFront.Data.StoreFrontDataEntities();
        // First screen of place order
        public ActionResult Address(string name)
        {
            //grab user's ID
            var user = dc.Users.Where(n => n.UserName == name).FirstOrDefault();
            var userID = user.UserID;

            //grab address information for user
            var addresses = dc.Addresses.Where(a => a.UserID == userID).ToList();
            return View(addresses);
        }
        //second screen
        public ActionResult ConfirmOrder()
        {
            return View();
        }
        //last screen
        public ActionResult OrderSubmitted()
        {
            return View();
        }
    }
}