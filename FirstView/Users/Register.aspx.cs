using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Users
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //txtName.Focus();
            }
            else
            {
                //txtPassword.Focus();
            }

        }

        //protected void txtUsername_TextChanged(object sender, EventArgs e)
        //{
        //    bool UsernameExists = false;

        //    cUsers usr = new cUsers();

        //    if (txtUsername.Text.Length > 0)
        //    {
        //        UsernameExists = usr.CheckUsernameExists(txtUsername.Text);
        //        if (UsernameExists == true)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalUsernameExists();", true);
        //        }
        //    }
        //    txtPassword.Focus();
        //}

        

         protected void butCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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
                    ArtistID = usr.Register(txtName.Text, txtSurname.Text, txtUsername.Text, txtPassword.Text, txtEmailAddress.Text, VerificationCode,
                        txtAddress.Text, txtTown.Text, txtPostCode.Text, txtTelephone.Text, txtMobile.Text, txtBankSortCode.Text.Trim(),txtBankAccountNumber.Text.Trim());
                    if (ArtistID > 0)
                    {
                        Session["ArtistID"] = ArtistID.ToString();
                        // Send verfication email
                        SendEmail(txtEmailAddress.Text, txtName.Text, txtSurname.Text, VerificationCode);
                        //Response.Redirect("RegisterVerify.aspx?reg=1", true);
                        Response.Redirect("ProfileEdit.aspx", true);
                    }
                    else
                    {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Error in registeration. Please try again.');", true); 
                    }
                }
            }
            txtPassword.Focus();
        }

        private void SendEmail(string EmailAddress, string Name, string Surname, string VerificationCode)
        {
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "First-View Gallery Notification - New Registration";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cSettings set = new cSettings();

            SmtpClient smtpClient = new SmtpClient();
            DataView dv = new DataView();

            FromEmailAddress = set.ListByCode("SystemEmailAddress");
            smtpClientHost = set.ListByCode("smtpClientHost");
            smtpClientPort = set.ListByCode("smtpClientPort");

            MailMessage message = new MailMessage();
            MailAddress mailFrom = new MailAddress(FromEmailAddress);
            message.To.Add(EmailAddress);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            sbBody.Clear();
            sbBody.AppendLine(@"<b>New Registration</b><br/><br/>");
            sbBody.AppendLine(String.Concat("Hello ", Name, " " , Surname,  " ", ",<br/><br/>"));
            sbBody.AppendLine(@"Thank you for registering on the First-View Gallery site. To complete your registration process you will need to verify your account. To do this please click on the link below.<br/>");
            sbBody.AppendLine(@"This process is required to ensure the validity of our users. If you have not registered on our site, please notify us at admin@first-view.uk <br/><br/>");
            sbBody.AppendLine(String.Concat("<a href='http://first-view.uk/users/RegisterVerify.aspx?reg=2&uc=",VerificationCode, "'> CLICK HERE TO VERIFY YOUR ACCOUNT.</a><br/><br/>"));
            sbBody.AppendLine(@"Thank You<br/>");
            sbBody.AppendLine(@"Admin @ First-View");

            message.Subject = Subject;
            message.Body = sbBody.ToString();

            smtpClient.Host = smtpClientHost;
            smtpClient.Port = Convert.ToInt32(smtpClientPort);

            smtpClient.Send(message);
            message.Dispose();
        }
    }
}