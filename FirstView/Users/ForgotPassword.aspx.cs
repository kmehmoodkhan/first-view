using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Users
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            string newPass = "";
            string DateTimeNow = "";
            string TempPassKey = "";
            cUsers usr = new cUsers();
            cRandomStringGenerator rsg = new cRandomStringGenerator(true, true, true, true);
            CommonLibrary cl = new CommonLibrary();

            DataView dv = new DataView();
            string email = txtEmailAddress.Text.Trim();
            bool UsernameExists = usr.CheckUsernameExists(txtUsername.Text, email);
            if (UsernameExists == true)
            {

                if (txtEmailAddress.Text.Length > 0)
                {
                    dv = usr.RecoveryByEmail(txtEmailAddress.Text);
                    if (dv.Table.Rows.Count > 0)
                    {
                        // Email Address is valid, send email with new password...valid for 15 minutes only
                        for (int i = 0; i < dv.Table.Rows.Count; i++)
                        {
                            TempPassKey = Guid.NewGuid().ToString();
                            newPass = rsg.Generate(8);
                            DateTimeNow = cl.FormatDateTime(System.DateTime.Now);
                            usr.RecoveryForgotPassword(txtEmailAddress.Text,txtUsername.Text, newPass, DateTimeNow, TempPassKey);

                            // Send Email
                            SendEmail(newPass, txtEmailAddress.Text, DateTimeNow, TempPassKey);
                        }
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalLogin();", true);
            }
        }

        private void SendEmail(string newPass, string EmailAddress, string DateTimeNow, string TempPassKey)
        {
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "First-View Gallery Management System - Password Recovery";
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

            sbBody.AppendLine("<b>First-View Gallery Management System</b><br/><br/>");
            sbBody.AppendLine(String.Concat("You have requested for your Password to be recovered. If you have not requested for your Password to be recovered, please <a href='http://first-view.uk/users/ResetPassword.aspx?tpk=", TempPassKey, "&nv=1'>click here</a>."));
            sbBody.AppendLine(String.Concat("Your temporary Password is <b>", newPass, "</b> generated on ", DateTimeNow, " and will be valid until ", cl.FormatDateTime(Convert.ToDateTime(DateTimeNow).AddMinutes(15))));
            sbBody.AppendLine("Please Note: This temporary Password is only valid for 15 minutes. If this time limit is exceeded a new temporary Pasword will have to be generated.<br/><br/>");
            sbBody.AppendLine(String.Concat("<a href='http://first-view.uk/users/ResetPassword.aspx?tpk=", TempPassKey, "'> Click here to reset your Password.</a><br/><br/>"));
            sbBody.AppendLine("Regards<br/>");
            sbBody.AppendLine("Admin @ First-View");

            message.Subject = Subject;
            message.Body = sbBody.ToString();

            smtpClient.Host = smtpClientHost;
            smtpClient.Port = Convert.ToInt32(smtpClientPort);

            smtpClient.Send(message);
            message.Dispose();
        }
    }
}