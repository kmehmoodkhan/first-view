using System;
using System.Data;
using System.Web.UI;

using FirstView.BusinessLayer;
using FirstView.Model;

namespace FirstView.Admin.Users
{
    public partial class PersonalDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!IsPostBack)
            {
                bindUserDetails();
            }
        }

        private void bindUserDetails()
        {
            cUsers usr = new cUsers();
            DataView dv = usr.ListByID(Convert.ToInt32(Session["FV_UserID"]));
            DataTable dt = dv.ToTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                txtUsername.Text = Convert.ToString(dt.Rows[0]["Username"]);
                txtName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                txtSurname.Text = Convert.ToString(dt.Rows[0]["Surname"]);
                txtEmailAddress.Text = Convert.ToString(dt.Rows[0]["Email"]);
                txtAddress.Text = Convert.ToString(dt.Rows[0]["Address1"]);
                txtTown.Text = Convert.ToString(dt.Rows[0]["Address2"]);
                txtPostCode.Text = Convert.ToString(dt.Rows[0]["ZipCode"]);
                txtTelephone.Text = Convert.ToString(dt.Rows[0]["HomePhone"]);
                txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
                txtBankSortCode.Text = Convert.ToString(dt.Rows[0]["BankSortCode"]);
                txtBankAccountNumber.Text = Convert.ToString(dt.Rows[0]["BankAccountNumber"]);
            }
        }

        private void CheckUserValidity()
        {
            //Reset session if expired
            if (Session["FV_UserID"] == null)
            {
                Response.Redirect("../Users/Login.aspx?im=3");
            }
            if (Session["FV_UserID"].ToString().Length == 0)
            {
                Response.Redirect("../Users/Login.aspx?im=3");
            }
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            bool UsernameExists = false;
            cUsers usr = new cUsers();
            try
            {
                if (Session["FV_UserID"] != null)
                {
                    if (txtUsername.Text.Length > 0)
                    {
                        UsernameExists = usr.CheckUsernameExists(txtUsername.Text, Convert.ToInt32(Session["FV_UserID"]));
                        if (UsernameExists == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalUsernameExists();", true);
                        }
                        else
                        {
                            UserModel objUserModel = new UserModel();

                            objUserModel.UserID = Convert.ToInt32(Session["FV_UserID"]);
                            objUserModel.Username = txtUsername.Text.Trim();
                            objUserModel.Password = txtPassword.Text.Trim();
                            objUserModel.Surname = txtSurname.Text.Trim();
                            objUserModel.Name = txtName.Text.Trim();
                            objUserModel.Email = txtEmailAddress.Text.Trim();
                            objUserModel.Address = txtAddress.Text.Trim();
                            objUserModel.Town = txtTown.Text.Trim();
                            objUserModel.ZipCode = txtPostCode.Text.Trim();
                            objUserModel.Telephone = txtTelephone.Text.Trim();
                            objUserModel.Mobile = txtMobile.Text.Trim();
                            objUserModel.BankSortCode = txtBankSortCode.Text.Trim();
                            objUserModel.BankAccountNumber = txtBankAccountNumber.Text.Trim();
                            usr.Edit(objUserModel);
                            lblMessage.Text = "Profile information updated successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        }
                    }
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Menu.aspx", true);
        }
    }
}