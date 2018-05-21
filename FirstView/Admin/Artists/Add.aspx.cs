using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Admin.Artists
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtName.Focus();
            }
            else
            {
                txtPassword.Focus();
            }

        }
        protected void butRegister_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;
            bool UsernameExists = false;
            string VerificationCode = "";
            DataView dv = new DataView();
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
                    VerificationCode = Guid.NewGuid().ToString();
                    ArtistID = usr.RegisterArtist(txtName.Text, txtSurname.Text, txtUsername.Text, txtPassword.Text, txtEmailAddress.Text, VerificationCode,
                        txtAddress.Text, txtTown.Text, txtPostCode.Text, txtTelephone.Text, txtMobile.Text, txtBankSortCode.Text.Trim(), txtBankAccountNumber.Text.Trim());

                    // Send verfication email
                    Response.Redirect("edit.aspx?ArtistID=" + ArtistID);
                }
            }
            txtPassword.Focus();
        }

        
    }
}