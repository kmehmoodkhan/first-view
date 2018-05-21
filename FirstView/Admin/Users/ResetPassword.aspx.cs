using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Admin.Users
{
    public partial class ResetPassword : System.Web.UI.Page
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
            string Name = "";
            string Surname = "";
            string EmailAddress = "";
            string Username = "";

            cArtist a = new cArtist();
            cUsers usr= new cUsers();
            DataView dv = new DataView();
            DataView dv1 = new DataView();
            
            if (Request.QueryString["UserID"] != null)
            {
                UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            }
            btnBack.HRef = "List.aspx";

            dv = usr.ListByID(UserID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Email"] != DBNull.Value)
                    { EmailAddress = dv.Table.Rows[i]["Email"].ToString(); }
                    if (dv.Table.Rows[i]["Username"] != DBNull.Value)
                    { Username = dv.Table.Rows[i]["Username"].ToString(); }                    
                }
            }

            lblUsername.Text = Username;
            lblName.Text = Name + " " + Surname;
            hidEmailAddress.Value = EmailAddress;
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            int UserID = 0;

            cUsers usr= new cUsers();

            if (Request.QueryString["UserID"] != null)
            {
                UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            }

            usr.ChangePassword(UserID, txtPassword.Text);

            if (chkSendEmail.Checked == true)
            {
                SendEmail(txtPassword.Text, lblName.Text);
            }
            Response.Redirect("List.aspx");
        }
        
        protected void butBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        private void SendEmail(string Pass, string Name)
        {
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "First-View Gallery Notification - Password Reset";
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
            message.To.Add(hidEmailAddress.Value);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            sbBody.Clear();
            sbBody.AppendLine(@"<b>Password Reset</b><br/><br/>");
            sbBody.AppendLine(String.Concat("Hello ", Name, ",<br/><br/>"));
            sbBody.AppendLine(String.Concat("Your password has been reset. Your new password is <b>", Pass, "</b><br/><br/>"));
            sbBody.AppendLine(@"Please use this password when logging into the First-View Gallery site.<br/>");
            sbBody.AppendLine(String.Concat("<a href='http://first-view.uk/users/Login.aspx'> Click here to login.</a><br/><br/>"));
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