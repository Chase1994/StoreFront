using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreFront.Data;

namespace StoreFront.ShippingApi.Controllers
{
    public class OrderController : ApiController
    {
        StoreFrontDataEntities dc = new StoreFrontDataEntities();
        OrderRepository or = new OrderRepository();

        //query is handled in order repository
        public List<Order> GetOrders(DateTime startDate, DateTime endDate)
        {
           
            List<Order> orders = or.GetOrders(startDate, endDate);
            return orders;
        }

        //handled in order repository
        public Boolean MarkOrderShipped(int id)
        {
            bool isShipped = or.ShipOrder(id);
            return isShipped;
        }
    }
}
