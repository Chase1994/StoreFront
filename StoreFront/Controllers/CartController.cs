using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;
using System.Data.Entity.Validation;
using System.Web.Security;

namespace StoreFront.Controllers
{
    public class CartController : Controller
    {
        private StoreFrontDBEntities dc = new StoreFrontDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            //get userID
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;
            //get user's shopping cart
            ShoppingCarts cart = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            var cartID = cart.ShoppingCartID;

            //grab the products in the cart
            
            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cartID);
            return View(cartList.ToList());
        }

        public ActionResult Delete(ShoppingCartProducts cartProd)
        {
            var cartItemID = cartProd.ShoppingCartProductID;
            ShoppingCartProducts itemToDelete = dc.ShoppingCartProducts.Where(a => a.ShoppingCartProductID == cartItemID).FirstOrDefault();
            if (itemToDelete.Quantity > 1)
            {
                itemToDelete.Quantity--;
                dc.SaveChanges();
            }
            else
            {
                dc.ShoppingCartProducts.Attach(itemToDelete);
                dc.ShoppingCartProducts.Remove(itemToDelete);
                dc.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult GetQuantity()
        {
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;
            //get user's shopping cart
            ShoppingCarts cart = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            if (cart == null)
            {
                return Json(0);
            }
            var cartID = cart.ShoppingCartID;

            //grab the products in the cart
            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cartID);
            var numItemsInCart = 0;
            //loop through the ShoppingCartProducts and add up quantity
            foreach (ShoppingCartProducts cartProds in cartList)
            {
                numItemsInCart += cartProds.Quantity;
            }
            return Json(numItemsInCart);
        }
    }
}