using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFront.Controllers
{
    public class PlaceOrderController : Controller
    {
        // First screen of place order
        public ActionResult Address()
        {
            return View();
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