using StoreFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Data;
namespace StoreFront.Controllers
{
    public class PlaceOrderController : Controller
    {
        StoreFront.Data.StoreFrontDataEntities dc = new StoreFront.Data.StoreFrontDataEntities();
        // First screen of place order
        [Authorize]
        public ActionResult Address(string name)
        {
            //grab user's ID
            var userID = getUserID();

            //grab address information for user
            var addresses = dc.Addresses.Where(a => a.UserID == userID);

            //user addresses
            ViewBag.Addresses = new SelectList(addresses, "AddressID", "Address1");

            //state dropdown stuff
            ViewBag.States = new SelectList(dc.States, "StateID", "StateName");
            return View();
        }
        //second screen

        public ActionResult SelectedAddress([Bind(Exclude = "IsBilling, IsShipping, DateModified, ModifiedBy, Address1, Address2, Address3")]Address selectedAddress)
        {
            //store addressID in a variable to pass to action
            var selectedAddressID = selectedAddress.AddressID;
            return RedirectToAction("ConfirmOrder", "PlaceOrder", new { addressID = selectedAddressID });
        }

        public ActionResult ConfirmOrder(int addressID)
        {
            //get userID
            var userID = getUserID();
            //grab user's shopping cart
            var userCart = dc.ShoppingCarts.Where(u => u.UserID == userID).FirstOrDefault();
            var userCartItems = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == userCart.ShoppingCartID).ToList();
            //store addressID selected in viewbag 
            ViewBag.AddressID = addressID;
            //pass model to view
            return View(userCartItems);
        }

        //last screen
        public ActionResult SubmitOrder(int id, double total)
        {
            //set variables to use for adding new order record
            var userID = getUserID();
            var addressID = id;
            var timeNow = System.DateTime.Now;

            //create new order for user
            Order newOrder = new Order();
            newOrder.UserID = userID;
            newOrder.AddressID = addressID;
            newOrder.OrderDate = timeNow;
            newOrder.Total = Convert.ToDecimal(total);
            newOrder.StatusID = 1;

            //save order to database
            dc.Orders.Add(newOrder);
            dc.SaveChanges();

            //grab user's current cart and list of items in cart
            var cart = dc.ShoppingCarts.Where(u => u.UserID == userID).FirstOrDefault();
            var cartList = dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cart.ShoppingCartID).ToList();

            //create an orderproduct for each product in user's cart
            foreach (var item in cartList)
            {
                var orderProd = new OrderProduct();
                orderProd.OrderID = newOrder.OrderID;
                orderProd.ProductID = item.ProductID;
                orderProd.Quantity = item.Quantity;
                orderProd.Price = item.Product.Price;
                dc.OrderProducts.Add(orderProd);
                dc.SaveChanges();
            }

            //remove all items from user's cart
            dc.ShoppingCartProducts.RemoveRange(dc.ShoppingCartProducts.Where(c => c.ShoppingCartID == cart.ShoppingCartID));
            dc.SaveChanges();

            return View();
        }

        public int getUserID()
        {
            //grab logged in username from Identity
            var user = dc.Users.Where(u => u.UserName.Equals(HttpContext.User.Identity.Name)).FirstOrDefault();
            var userID = user.UserID;
            return userID;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Address([Bind(Exclude = "IsBilling, IsShipping, DateModified, ModifiedBy")]Address newAddress)
        {
            bool Status = false;
            string message = "";

            if (ModelState.IsValid)
            {
                //for record keeping purposes
                newAddress.CreatedBy = HttpContext.User.Identity.Name;
                newAddress.DateCreated = System.DateTime.Now;

                //get userID
                newAddress.UserID = getUserID();

                using (dc)
                {
                    try
                    {
                        dc.Addresses.Add(newAddress);
                        dc.SaveChanges();
                        message = " You have successfully added a new address.";
                        Status = true;
                    }
                    catch
                    {
                        message = "Errorrrr";
                        ViewBag.Message = message;
                        return View();
                    }
                }

            }
            else
            {
                message = "Error, check inputs.";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            var newAddressID = newAddress.AddressID;
            return RedirectToAction("ConfirmOrder", "PlaceOrder", new { addressID = newAddressID });
        }
    }
}