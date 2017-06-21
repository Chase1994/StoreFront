using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    public class InventoryRepository
    {
        StoreFrontDataEntities db = new StoreFrontDataEntities();
        public List<Product> SearchProducts(string searchText)
        {
            List<Product> prodList = db.Products.Where(a => a.ProductName.Contains(searchText) || a.ProductDescription.Contains(searchText)).ToList();
            return prodList;
        }

        public Product GetProduct(int id)
        {
            Product prod = db.Products.Where(a => a.ProductID == id).FirstOrDefault();
            return prod;
        }

    }
}
