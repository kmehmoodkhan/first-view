using System;
using System.Web.UI;
using FirstView.BusinessLayer;
namespace FirstView.Users
{
    public partial class RegisterVerify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string VerificationCode = "";
            int IsVerified = 0;
            cUsers usr = new cUsers();

            reg1.Style["Display"] = "none";
            reg2.Style["Display"] = "none";
            reg3.Style["Display"] = "none";

            if (Request.QueryString["reg"] != null)
            {
                if (Request.QueryString["reg"].ToString() == "1")
                {
                    reg1.Style["Display"] = "";
                }

                if (Request.QueryString["reg"].ToString() == "2")
                {
                    // Check verification
                    if (Request.QueryString["uc"] != null)
                    {
                        VerificationCode = Request.QueryString["uc"].ToString();
                    }
                    IsVerified = usr.RegisterCheckVerify(VerificationCode);
                    if (IsVerified == 0)
                    {
                        usr.RegisterVerify(VerificationCode);
                        reg2.Style["Display"] = "";
                    }
                    else
                    {
                        reg3.Style["Display"] = "";
                    }
                }
            }
        }
    }
}