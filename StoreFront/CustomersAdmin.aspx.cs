using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreFront
{
    public partial class CustomersAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UserGrid_ItemInserting(object sender, DetailsViewInsertedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
           "alert", "alert('User Successfully Added');", true);
            UserGrid.DataBind();
        }
    }
}