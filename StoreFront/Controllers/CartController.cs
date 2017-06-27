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
        [Authorize]
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

        public ActionResult Delete(int cartItemID)
        {
            ShoppingCartProducts itemToDelete = dc.ShoppingCartProducts.Where(a => a.ShoppingCartProductID == cartItemID).FirstOrDefault();
     
            dc.ShoppingCartProducts.Remove(itemToDelete);
            dc.SaveChanges();

            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;
            ShoppingCarts cart = dc.ShoppingCarts.FirstOrDefault(a => a.UserID == userID);
            var cartTotal = cart.ShoppingCartProducts.Sum(c => c.Products.Price * c.Quantity);

            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cart.ShoppingCartID);
            var numItems = numItemsInCart(cart.ShoppingCartID);
            return Json(new { cartTotal, numItems });
        }

        [HttpPost]
        public ActionResult Update(int cartProdID, int newQuantity)
        {

            ShoppingCartProducts cartProd = dc.ShoppingCartProducts.Where(p => p.ShoppingCartProductID == cartProdID).FirstOrDefault();

            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;

            ShoppingCarts cart = dc.ShoppingCarts.FirstOrDefault(a => a.UserID == userID);

            if (newQuantity < 1)
            {
                return Json(new { newQuantity });
            }
            else
            {
                cartProd.Quantity = newQuantity;
            }
            dc.SaveChanges();

            var newSubtotal = cartProd.Products.Price * newQuantity;
            var cartTotal = cart.ShoppingCartProducts.Sum(c => c.Products.Price * c.Quantity);

            var numItems = numItemsInCart(cart.ShoppingCartID);

            return Json(new {newQuantity, newSubtotal, cartTotal, numItems });
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
            var numItems = numItemsInCart(cartID);
            
            return Json(numItems);
        }

        [NonAction]
        public int? numItemsInCart(int cartID)
        {
            //grab the products in the cart
            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cartID);
            int? numItemsInCart = 0;
            //loop through the ShoppingCartProducts and add up quantity
            foreach (ShoppingCartProducts cartProds in cartList)
            {
                numItemsInCart += cartProds.Quantity;
            }
            return numItemsInCart;
        }
    }
}