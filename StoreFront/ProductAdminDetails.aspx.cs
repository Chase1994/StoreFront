using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using StoreFront.Data;

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
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            StoreFront.Data.StoreFrontDataEntities dc = new StoreFront.Data.StoreFrontDataEntities();
            if (ProductImageUpload.HasFile)
            {
                try
                {
                    string q = Request.QueryString["ProductID"];
                    string imgName = Path.GetFileName(ProductImageUpload.FileName);
                    ProductImageUpload.SaveAs(Server.MapPath("~/Images/") + imgName);

                    dc.Database.ExecuteSqlCommand("UPDATE Products SET ImageFile = {0} WHERE(ProductID = {1})", imgName, q);
                    StatusLabel.Text = "Upload status: File uploaded!";

                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

    }
}