using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreFront.Data;
using StoreFront.ShippingApi;

namespace StoreFront.UnitTest
{
    [TestClass]
    public class APIUnitTest
    {
        ShippingApi.Controllers.OrderController oc = new ShippingApi.Controllers.OrderController();
        StoreFrontDataEntities db = new StoreFrontDataEntities();

        //correct parameters
        [TestMethod]
        public void GetOrders_Correct()
        {
            //arrange
            DateTime start = System.DateTime.Now.AddDays(-30);
            DateTime end = System.DateTime.Now;
            //act
            var list = oc.GetOrders(start, end);
            //assert
            Assert.IsNotNull(list);
        }

        //start date > end date
        [TestMethod]
        public void GetOrders_InvalidDates()
        {
            //arrange
            DateTime start = System.DateTime.Now.AddDays(30);
            DateTime end = System.DateTime.Now;
            var expectedListCount = 0;
            //act
            var list = oc.GetOrders(start, end);
            //assert
            Assert.AreEqual(expectedListCount, list.Count);
        }

        //correct parameters
        [TestMethod]
        public void MarkOrderShipped_Correct()
        {
            //arrange
            Order newOrder = new Order();
            newOrder.AddressID = 3;
            newOrder.UserID = 17;
            newOrder.StatusID = 1;
            db.Orders.Add(newOrder);
            db.SaveChanges();

            var expected = true;

            //act
            bool isStatusIncreased = oc.MarkOrderShipped(newOrder.OrderID);
            //assert
            Assert.AreEqual(expected, isStatusIncreased);
        }

        //order already is shipped
        [TestMethod]
        public void MarkOrderShipped_StatusAlreadyShipped()
        {
            //arrange
            Order newOrder = new Order();
            newOrder.AddressID = 3;
            newOrder.UserID = 17;
            newOrder.StatusID = 4;
            db.Orders.Add(newOrder);
            db.SaveChanges();

            var expected = true;

            //act
            bool isStatusIncreased = oc.MarkOrderShipped(newOrder.OrderID);
            //assert
            Assert.AreEqual(expected, isStatusIncreased);
        }

        //order does not exist
        [TestMethod]
        public void MarkOrderShipped_OrderNotExists()
        {
            //arrange
            Order newOrder = new Order();
            newOrder.AddressID = 3;
            newOrder.UserID = 17;
            newOrder.StatusID = 4;
            db.Orders.Add(newOrder);
            db.SaveChanges();

            db.Orders.Remove(newOrder);
            db.SaveChanges();

            var expected = false;

            //act
            bool isStatusIncreased = oc.MarkOrderShipped(newOrder.OrderID);
            //assert
            Assert.AreEqual(expected, isStatusIncreased);
        }
    }
}
