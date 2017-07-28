using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using StoreFront.Data;
namespace StoreFront.InventoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        InventoryRepository ir = new InventoryRepository();

        public List<ProductItem> GetProductDetails(int id)
        {
            if (id.Equals(null) || id <= 0)
            {
                throw new ArgumentNullException("id");
            }
            else
            {
                //initiate a new data contract object
                ProductItem theProduct = new ProductItem();

                //grab product to map in entity
                Product dcProd = ir.GetProduct(id);

                //map entity Product to data contract ProductItem
                theProduct.ProductID = dcProd.ProductID;
                theProduct.ProductName = dcProd.ProductName;
                theProduct.ProductDescription = dcProd.ProductDescription;
                theProduct.Price = dcProd.Price;
                theProduct.Quantity = dcProd.Quantity;

                //ghetto hack to display result in gridview because result needs to be of type IEnumerable/IList
                List<ProductItem> theList = new List<ProductItem>();
                theList.Add(theProduct);

                return theList;
            }
        }

        public List<ProductItem> SearchProducts(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            else
            {
                List<Product> prodList = ir.SearchProducts(value);
                List<ProductItem> listToReturn = new List<ProductItem>();
                foreach (var item in prodList)
                {
                    ProductItem theProduct = new ProductItem();

                    theProduct.ProductID = item.ProductID;
                    theProduct.ProductName = item.ProductName;
                    theProduct.ProductDescription = item.ProductDescription;
                    theProduct.Price = item.Price;
                    theProduct.Quantity = item.Quantity;

                    listToReturn.Add(theProduct);
                }
                return listToReturn;
            }
        }
    }
}
