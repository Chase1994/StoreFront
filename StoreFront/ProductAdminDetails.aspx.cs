using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreFront
{
    public partial class ProductAdminDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ProductView_OnUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
           "alert", "alert('Product Successfully Updated');", true);
            ProductView.DataBind();
        }
        protected void BackToProducts_OnDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('Product Successfully Deleted');" +
            "window.location ='ProductsAdmin.aspx';", true);
        }
    }
}