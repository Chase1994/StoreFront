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

        public ActionResult AddProduct(Products prod)
        {
            Users user = dc.Users.Where(a => a.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            var userID = user.UserID;
            var exists = doesUserCartExist(userID);

            //if cart doesn't exist yet for user, create one
            if (!exists)
            {
                ShoppingCarts cartToAdd = new ShoppingCarts();
                cartToAdd.UserID = userID;
                cartToAdd.DateCreated = System.DateTime.Now;
                cartToAdd.CreatedBy = user.UserName;

                dc.ShoppingCarts.Add(cartToAdd);
                dc.SaveChanges();
            }
            //if cart exists, grab the cart from database
            ShoppingCarts cart = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            var cartID = cart.ShoppingCartID;

            //make new product to add to cart
            var cartProdExists = isItemInCart(cartID, prod.ProductID);

            if (cartProdExists)
            {
                ShoppingCartProducts existProd = dc.ShoppingCartProducts.Where(a => a.ShoppingCartID == cartID).Where(a => a.ProductID == prod.ProductID).FirstOrDefault();
                existProd.Quantity++;
                //push updated ShoppingCartProduct to database

                dc.SaveChanges();
            }
            else
            {
                ShoppingCartProducts cartProd = new ShoppingCartProducts();
                cartProd.ShoppingCartID = cartID;
                cartProd.ProductID = prod.ProductID;
                cartProd.Quantity = 1;
                dc.ShoppingCartProducts.Add(cartProd);
                dc.SaveChanges();

            }

            return RedirectToAction("Index", "Products");
        }

        [NonAction]
        public bool doesUserCartExist(int userID)
        {
            var v = dc.ShoppingCarts.Where(a => a.UserID == userID).FirstOrDefault();
            return v != null;
        }

        [NonAction]
        public bool isItemInCart(int cartID, int prodID)
        {
            var v = dc.ShoppingCartProducts.Where(a => a.ShoppingCartID == cartID).Where(a => a.ProductID == prodID).FirstOrDefault();
            return v != null;
        }
    }
}