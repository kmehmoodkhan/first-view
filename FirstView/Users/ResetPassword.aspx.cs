using System;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Users
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            int Result = 0;
            cUsers usr = new cUsers();

            string TempPassKey = "";
            if (Request.QueryString["tpk"] != null)
            {
                TempPassKey = Request.QueryString["tpk"].ToString();
            }
            if (Request.QueryString["nv"] != null)
            {
                if (Request.QueryString["nv"].ToString() == "1")
                {                    
                    Result = usr.UndoPasswordReset(TempPassKey);
                    if (Result == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRecovery();", true);
                    }
                    else if (Result == 1)
                    {
                        Response.Redirect("Login.aspx?im=2");
                    }                    
                }
            }
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            int Result = 0;
            string TempPassKey = "";
            cUsers usr = new cUsers();

            if (Request.QueryString["tpk"] != null)
            {
                TempPassKey = Request.QueryString["tpk"].ToString();
            }

            Result = usr.SetNewPassword(txtTempPassword.Text, TempPassKey, txtNewPassword.Text);
            if (Result == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRecovery();", true);
            }
            else if (Result == 1)
            {
                Response.Redirect("Login.aspx?im=1");
            }

        }
    }
}