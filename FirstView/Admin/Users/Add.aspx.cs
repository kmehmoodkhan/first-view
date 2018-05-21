using System;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.Users
{
    public partial class Add : System.Web.UI.Page
    {
        public static string FileName = "";
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
            if (Request.QueryString["RetUrl"] != null)
            {
                if (Request.QueryString["RetUrl"].ToString() == "1")
                {
                    chkArtist.Checked = true;
                    chkArtist.Enabled = false;
                    chkAdmin.Checked = false;
                    chkAdmin.Enabled = false;
                    btnBack.HRef = "../Artists/List.aspx";
                }
            }
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;
            bool UsernameExists = false;

            cUsers usr = new cUsers();

            if (txtUsername.Text.Length > 0)
            {
                UsernameExists = usr.CheckUsernameExists(txtUsername.Text);
                if (UsernameExists == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalUsernameExists();", true);
                }
                else
                {
                    if (chkArtist.Checked == true)
                    {
                        ArtistID = usr.Add2(txtName.Text, txtSurname.Text, txtEmailAddress.Text, txtUsername.Text,
                            txtPassword.Text, true, chkAdmin.Checked, chkArtist.Checked, 
                            txtAddress.Text, txtTown.Text, txtPostCode.Text, txtTelephone.Text, txtMobile.Text,txtBankSortCode.Text.Trim(), txtBankAccountNumber.Text.Trim());

                        if (Request.QueryString["RetUrl"] != null)
                        {
                            if (Request.QueryString["RetUrl"].ToString() == "1")
                            {
                                Response.Redirect("../Artists/Edit.aspx?ArtistID=" + ArtistID.ToString());
                            }
                        }
                    }
                    else
                    {
                        usr.Add(txtName.Text, txtSurname.Text, txtEmailAddress.Text, txtUsername.Text,
                            txtPassword.Text, true, chkAdmin.Checked,
                            txtAddress.Text, txtTown.Text, txtPostCode.Text, txtTelephone.Text, txtMobile.Text,txtBankSortCode.Text.Trim(),txtBankAccountNumber.Text.Trim());
                    }

                    Response.Redirect("List.aspx");
                }
            }
            txtPassword.Focus();
        }
    }
}