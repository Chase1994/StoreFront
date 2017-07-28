using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using StoreFront.Models;

namespace StoreFront.Controllers
{
    public class AddToCartController : Controller
    {
        StoreFrontDBEntities dc = new StoreFrontDBEntities();

        public ActionResult AddProduct(int prodID)
        {
            //grab the user that is logged in
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            
            //if there is no one logged in, don't do anything
            if (user == null)
            {
                return View();
            }

            //grab the userID and check to see if the user has a cart associated with their account
            var userID = user.UserID;
            var exists = doesUserCartExist(userID);

            //if cart doesn't exist yet for user, create one
            if (!exists)
            {
                ShoppingCarts cartToAdd = new ShoppingCarts();
                cartToAdd.UserID = userID;
                cartToAdd.DateCreated = System.DateTime.Now;
                cartToAdd.CreatedBy = user.UserName;

                //add the new cart to the database
                dc.ShoppingCarts.Add(cartToAdd);
                dc.SaveChanges();
            }

            //if cart exists, grab the cart from database
            ShoppingCarts cart = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            var cartID = cart.ShoppingCartID;

            //make new product to add to cart
            var cartProdExists = isItemInCart(cartID, prodID);

            //if the product is already in the cart, update quantity instead of adding a new item
            if (cartProdExists)
            {
                ShoppingCartProducts existProd = dc.ShoppingCartProducts.Where(a => a.ShoppingCartID == cartID).Where(a => a.ProductID == prodID).FirstOrDefault();
                existProd.Quantity++;

                //push updated ShoppingCartProduct to database
                dc.SaveChanges();
            }

            //if the product doesn't exist in the cart already, create a new Shopping Cart Product
            else
            {
                ShoppingCartProducts cartProd = new ShoppingCartProducts();
                cartProd.ShoppingCartID = cartID;
                cartProd.ProductID = prodID;
                cartProd.Quantity = 1;

                //save the ShoppingCartProduct
                dc.ShoppingCartProducts.Add(cartProd);
                dc.SaveChanges();

            }
            //grab number of items in cart
            int? numItems = numItemsInCart(cartID);
            
            return Json(numItems);
        }

        [NonAction]
        public bool doesUserCartExist(int userID)
        {
            //return true if cart exists for user
            var v = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            return v != null;
        }

        [NonAction]
        public bool isItemInCart(int cartID, int prodID)
        {
            //return true if shopping cart product is already in cart
            var v = dc.ShoppingCartProducts.Where(a => a.ShoppingCartID == cartID).Where(a => a.ProductID == prodID).FirstOrDefault();
            return v != null;
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