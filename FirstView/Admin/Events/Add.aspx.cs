using System;
using System.Web.UI;
namespace FirstView.Admin.Events
{
    public partial class Add : System.Web.UI.Page
    {
        public static string FileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                hidUniqueID.Value = Guid.NewGuid().ToString();
                ifrmUpload.Src = "../../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;
            }
        }

        private void CheckUserValidity()
        {
            //Reset session if expired
            if (Session["FV_UserID"] == null)
            {
                Response.Redirect("../../Users/Login.aspx?im=3");
            }
            if (Session["FV_UserID"].ToString().Length == 0)
            {
                Response.Redirect("../../Users/Login.aspx?im=3");
            }
        }
    }
}