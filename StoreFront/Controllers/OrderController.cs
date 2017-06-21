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
        // GET: Order
        public ActionResult OrdersAdmin()
        {
            var orders = from o in dc.Orders
                         select o;
            return View(orders);

        }
        // GET: Order/Details/5
        public ActionResult OrderAdminDetails(int id)
        {
            Order editOrder = dc.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            return View(editOrder);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public PartialViewResult OutputProductPartial(Order order)
        {
            var id = order.OrderID;
            var orderProductsList = dc.OrderProducts.Where(o => o.OrderID == id);
            return PartialView("~/Views/Order/_ProductPartial.cshtml", orderProductsList);
        }

        [HttpPost]
        public ActionResult UpdateOrder(int orderProdID, int newQuantity)
        {

            OrderProduct theOrderProd = dc.OrderProducts.Where(o => o.OrderProductID == orderProdID).FirstOrDefault();
           
            if (newQuantity < 1)
            {
                return Json(new { newQuantity });
            }
            else
            {
                theOrderProd.Quantity = newQuantity;
            }
            dc.SaveChanges();

            var newSubtotal = theOrderProd.Product.Price * newQuantity;
            return Json(new { newQuantity, newSubtotal });
        }
        [HttpPost]
        public ActionResult RemoveOrderProduct(int orderProdID)
        {
            OrderProduct prodToRemove = dc.OrderProducts.Where(o => o.OrderProductID == orderProdID).FirstOrDefault();

            dc.OrderProducts.Remove(prodToRemove);
            dc.SaveChanges();

            return Json(orderProdID);
        }

        [HttpPost]
        public ActionResult UpStatus(int orderID, int currentStatusID)
        {
            Order currOrder = dc.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            var newStatus = "";
            if (currentStatusID == 4)
            {
                newStatus = currOrder.Status.StatusDescription;
                return Json(newStatus);
            }
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