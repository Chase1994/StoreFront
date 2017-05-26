using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreFront
{
    public partial class CustomerAdminDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void UserInfoDisplayGrid_ItemUpdate(object sender, DetailsViewUpdatedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('User Successfully Updated');", true);
            UserInfoDisplayGrid.DataBind();
        }
        protected void BackToUsers_OnDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('User Successfully Deleted');" +
            "window.location ='CustomersAdmin.aspx';", true);
        }
    }
}