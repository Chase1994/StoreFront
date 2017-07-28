using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StoreFront.InventoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<ProductItem> GetProductDetails(int id);

        [OperationContract]
        List<ProductItem> SearchProducts(string value);

        // TODO: Add your service operations here
    }

    [DataContract]
    public class ProductItem
    {
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        public int? Quantity { get; set; }

        [DataMember]
        public decimal? Price { get; set; }
    }
}
