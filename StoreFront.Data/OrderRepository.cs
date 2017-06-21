using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    class OrderRepository
    {
        StoreFrontDataEntities db = new StoreFrontDataEntities();
        public Order GetOrder(int id)
        {
            Order order = db.Orders.Where(a => a.OrderID == id).FirstOrDefault();
            return order;
        }
    }
}
