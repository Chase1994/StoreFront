using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace StoreFront
{
    public partial class WebServiceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            String searchText = SearchTextBox.Text;
            InventoryServiceReference.Service1Client client = new InventoryServiceReference.Service1Client();

            SearchGridView.DataSource = client.SearchProducts(searchText);
            SearchGridView.DataBind();
        }

        protected void DetailButton_Click(object sender, EventArgs e)
        {
            int detailID = DetailTextBox.Text.AsInt();
            InventoryServiceReference.Service1Client client = new InventoryServiceReference.Service1Client();
            var result = client.GetProductDetails(detailID);

            DetailGridView.DataSource = result;
            DetailGridView.DataBind();
        }
    }
}