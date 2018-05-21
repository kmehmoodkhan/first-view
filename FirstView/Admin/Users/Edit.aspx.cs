using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.Users
{
    public partial class Edit : System.Web.UI.Page
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
            int UserID = 0;
            int ArtistID = 0;
            string Name = "";
            string Surname = "";

            cArtist a = new cArtist();
            cUsers usr= new cUsers();
            DataView dv = new DataView();
            DataView dv1 = new DataView();
            
            if (Request.QueryString["UserID"] != null)
            {
                UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            }
            btnBack.HRef = "List.aspx?UserID=" + UserID.ToString();

            dv = usr.ListByID(UserID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { txtName.Text = dv.Table.Rows[i]["Name"].ToString(); }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { txtSurname.Text = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Email"] != DBNull.Value)
                    { txtEmailAddress.Text = dv.Table.Rows[i]["Email"].ToString(); }
                    if (dv.Table.Rows[i]["Username"] != DBNull.Value)
                    { lblUsername.Text = dv.Table.Rows[i]["Username"].ToString(); }
                    if (dv.Table.Rows[i]["IsAdmin"] != DBNull.Value)
                    { chkAdmin.Checked = Convert.ToBoolean(dv.Table.Rows[i]["IsAdmin"]); }
                    if (dv.Table.Rows[i]["IsActive"] != DBNull.Value)
                    { chkActive.Checked = Convert.ToBoolean(dv.Table.Rows[i]["IsActive"]); }
                    if (dv.Table.Rows[i]["IsVerified"] != DBNull.Value)
                    { chkVerified.Checked = Convert.ToBoolean(dv.Table.Rows[i]["IsVerified"]); }
                    if (dv.Table.Rows[i]["IsArtist"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dv.Table.Rows[i]["IsArtist"]) == true)
                        {
                            ArtistID = Convert.ToInt32(dv.Table.Rows[i]["ArtistID"]);
                            dv1 = a.ListByID(ArtistID);
                            if (dv1.Table.Rows.Count > 0)
                            {
                                for (int j = 0; j < dv1.Table.Rows.Count; j++)
                                {
                                    if (dv1.Table.Rows[j]["Name"] != DBNull.Value)
                                    { Name = dv1.Table.Rows[j]["Name"].ToString(); }
                                    if (dv1.Table.Rows[j]["Surname"] != DBNull.Value)
                                    { Surname = dv1.Table.Rows[j]["Surname"].ToString(); }
                                }
                            }

                            lblIsArtist.Text = "Yes [" + Name + " " + Surname + "]";
                        }
                        else if (Convert.ToBoolean(dv.Table.Rows[i]["IsArtist"]) == false)
                        { lblIsArtist.Text = "No"; }
                    }
                    if (dv.Table.Rows[i]["Address1"] != DBNull.Value)
                    { txtAddress.Text = dv.Table.Rows[i]["Address1"].ToString(); }
                    if (dv.Table.Rows[i]["Address2"] != DBNull.Value)
                    { txtTown.Text = dv.Table.Rows[i]["Address2"].ToString(); }
                    if (dv.Table.Rows[i]["ZipCode"] != DBNull.Value)
                    { txtPostCode.Text = dv.Table.Rows[i]["ZipCode"].ToString(); }
                    if (dv.Table.Rows[i]["HomePhone"] != DBNull.Value)
                    { txtTelephone.Text = dv.Table.Rows[i]["HomePhone"].ToString(); }
                    if (dv.Table.Rows[i]["Mobile"] != DBNull.Value)
                    { txtMobile.Text = dv.Table.Rows[i]["Mobile"].ToString(); }
                    txtBankSortCode.Text = Convert.ToString(dv.Table.Rows[0]["BankSortCode"]);
                    txtBankAccountNumber.Text = Convert.ToString(dv.Table.Rows[0]["BankAccountNumber"]);
                }
            }
            rfvConfirmPassword.Enabled = false;
            rfvPassword.Enabled = false; 
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            int UserID = 0;
            string Surname = "";
            string Name= "";
            string EmailAddress = "";
            bool IsAdmin = false;
            bool IsActive = false;
            bool IsVerified = false;
            string Pass = "";

            cUsers usr= new cUsers();

            if (Request.QueryString["UserID"] != null)
            {
                UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            }
            if (txtName.Text.Length > 0)
            {
                Name = txtName.Text;
            }
            if (txtSurname.Text.Length > 0)
            {
                Surname= txtSurname.Text;
            }
            if (txtEmailAddress.Text.Length > 0)
            {
                EmailAddress = txtEmailAddress.Text;
            }
            IsActive = chkActive.Checked;
            IsAdmin = chkAdmin.Checked;
            IsVerified = chkVerified.Checked;

            if (chkChangePass.Checked == true)
            {
                Pass = txtPassword.Text;
            }

           usr.Edit(UserID, Name, Surname, EmailAddress, chkChangePass.Checked, Pass, IsActive, IsAdmin, IsVerified,
               txtAddress.Text, txtTown.Text, txtPostCode.Text, txtTelephone.Text, txtMobile.Text, txtBankSortCode.Text.Trim(), txtBankAccountNumber.Text.Trim());

            Response.Redirect("List.aspx");
        }
        
        protected void butBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        protected void chkChangePass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangePass.Checked == true)
            {
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                rfvConfirmPassword.Enabled = true;
                rfvPassword.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                rfvConfirmPassword.Enabled = false;
                rfvPassword.Enabled = false;
            }
        }
    }
}