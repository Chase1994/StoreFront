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
            //grab the shopping cart product that is going to be deleted
            ShoppingCartProducts itemToDelete = dc.ShoppingCartProducts.Where(a => a.ShoppingCartProductID == cartItemID).FirstOrDefault();
            
            //remove the item from database
            dc.ShoppingCartProducts.Remove(itemToDelete);
            dc.SaveChanges();

            //grab the user who is logged in
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;

            //grab user's shopping cart, and calculate new total price
            ShoppingCarts cart = dc.ShoppingCarts.FirstOrDefault(a => a.UserID == userID);
            var cartTotal = cart.ShoppingCartProducts.Sum(c => c.Products.Price * c.Quantity);

            //get number of items in the cart
            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cart.ShoppingCartID);
            var numItems = numItemsInCart(cart.ShoppingCartID);

            //return new Json object with the new total and number of items in cart
            return Json(new { cartTotal, numItems });
        }

        [HttpPost]
        public ActionResult Update(int cartProdID, int newQuantity)
        {
            //get the shopping cart product to be updated
            ShoppingCartProducts cartProd = dc.ShoppingCartProducts.Where(p => p.ShoppingCartProductID == cartProdID).FirstOrDefault();

            //get the id of the user who is logged in
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;

            //grab the user's shopping cart
            ShoppingCarts cart = dc.ShoppingCarts.FirstOrDefault(a => a.UserID == userID);

            //return without changes if the quantity is 0 or negative
            if (newQuantity < 1)
            {
                return Json(new { newQuantity });
            }
            else
            {
                cartProd.Quantity = newQuantity;
            }
            dc.SaveChanges();

            //calculate new subtotal for the product, new cart total, and number of items in the cart.
            var newSubtotal = cartProd.Products.Price * newQuantity;
            var cartTotal = cart.ShoppingCartProducts.Sum(c => c.Products.Price * c.Quantity);
            var numItems = numItemsInCart(cart.ShoppingCartID);

            return Json(new {newQuantity, newSubtotal, cartTotal, numItems });
        }

        [HttpPost]
        public ActionResult GetQuantity()
        {
            //get user who is logged in
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
            int? numItemsInCart = cartList.Sum(a => a.Quantity);
            return numItemsInCart;
        }
    }
}