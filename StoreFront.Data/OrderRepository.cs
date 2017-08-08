using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    public class OrderRepository
    {
        StoreFrontDataEntities db = new StoreFrontDataEntities();

        public Order GetOrder(int id)
        {
            Order order = db.Orders.Where(a => a.OrderID == id).FirstOrDefault();
            return order;
        }

        public List<Order> GetOrders(DateTime startDate, DateTime endDate)
        {
            var orderList = new List<Order>();
            if (startDate >= endDate)
            {
                return orderList;
            }
            else
            {
                orderList = db.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
            }
            return orderList;
        }

        public Boolean ShipOrder(int id)
        {
            Order order = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            if (order != null)
            {
                order.StatusID = 4;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
