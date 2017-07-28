using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using PagedList;
using StoreFront.Data;

namespace StoreFront.Controllers
{
    public class OrderController : Controller
    {
        StoreFront.Data.StoreFrontDataEntities dc = new StoreFront.Data.StoreFrontDataEntities();
        
        [Authorize]
        public ActionResult OrdersAdmin()
        {
            return View(dc.Orders);
        }
     
        [Authorize]
        public ActionResult OrderAdminDetails(int id)
        {
            //grab order and pass to view based on the orderID
            Order editOrder = dc.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            return View(editOrder);
        }

        public PartialViewResult OutputProductPartial(Order order)
        {
            //create list of order products and pass to partial view
            var id = order.OrderID;
            var orderProductsList = dc.OrderProducts.Where(o => o.OrderID == id);
            return PartialView("~/Views/Order/_ProductPartial.cshtml", orderProductsList);
        }

        [HttpPost]
        public ActionResult UpdateOrder(int orderProdID, int newQuantity)
        {
            //grab the orderProduct to be updated
            OrderProduct theOrderProd = dc.OrderProducts.Where(o => o.OrderProductID == orderProdID).FirstOrDefault();
            
            //if the new quantity is 0 or negative, return back without making changes
            if (newQuantity < 1)
            {
                return Json(new { newQuantity });
            }

            //else set the orderProduct's quantity to the new quantity
            else
            {
                theOrderProd.Quantity = newQuantity;
            }
            dc.SaveChanges();

            //calculate new subtotal for the orderProduct
            var newSubtotal = theOrderProd.Product.Price * newQuantity;

            //return Json object with the new subtotal and quantity
            return Json(new { newQuantity, newSubtotal });
        }

        [HttpPost]
        public ActionResult RemoveOrderProduct(int orderProdID)
        {
            //grab the orderProduct that is to be removed
            OrderProduct prodToRemove = dc.OrderProducts.Where(o => o.OrderProductID == orderProdID).FirstOrDefault();

            //remove it
            dc.OrderProducts.Remove(prodToRemove);
            dc.SaveChanges();

            return Json(orderProdID);
        }

        [HttpPost]
        public ActionResult UpStatus(int orderID, int currentStatusID)
        {
            //grab the order that is to be updated
            Order currOrder = dc.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            var newStatus = "";

            //if currentStatus can't be increased, return without updating
            if (currentStatusID == 4)
            {
                newStatus = currOrder.Status.StatusDescription;
                return Json(newStatus);
            }
            //else increase the status and save to database
            else
            {
                currOrder.StatusID += 1;
                dc.SaveChanges();
                newStatus = currOrder.Status.StatusDescription;
                return Json(newStatus);
            }
        }

    }
}