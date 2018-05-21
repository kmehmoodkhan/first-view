using System;
using System.Web.UI;
using FirstView.BusinessLayer;
namespace FirstView.Admin.Settings
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                LoadData();
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

        private void LoadData()
        {
            cSettings set = new cSettings();

            txtSystemEmailAddress.Text = set.ListByCode("SystemEmailAddress");
            txtsmtpClientHost.Text = set.ListByCode("smtpClientHost");
            txtsmtpClientPort.Text = set.ListByCode("smtpClientPort");

        }       

        protected void butSave_Click(object sender, EventArgs e)
        {
            cSettings set = new cSettings();

            set.UpdateByCode("SystemEmailAddress", txtSystemEmailAddress.Text);
            set.UpdateByCode("smtpClientHost", txtsmtpClientHost.Text);
            set.UpdateByCode("smtpClientPort", txtsmtpClientPort.Text);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
        }
    }
}