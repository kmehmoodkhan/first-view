using System;
using System.Data;
using System.Net.Mail;
using System.Text;

using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            cUsers u = new cUsers();
            DataView dv = new DataView();

            // Send Email to All Admins
            dv = u.ListAllAdmins();
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    SendEmail(dv.Table.Rows[i]["Email"].ToString());
                }
            }
        }

        private void SendEmail(string EmailAddress)
        {
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cSettings set = new cSettings();
            DataView dv = new DataView();

            Subject = "First-View Gallery Notification - Contact Us - Feedback";

            SmtpClient smtpClient = new SmtpClient();
            FromEmailAddress = set.ListByCode("SystemEmailAddress");
            smtpClientHost = set.ListByCode("smtpClientHost");
            smtpClientPort = set.ListByCode("smtpClientPort");

            MailMessage message = new MailMessage();
            MailAddress mailFrom = new MailAddress(FromEmailAddress);
            message.To.Add(EmailAddress);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            sbBody.AppendLine("<b>Feedback Submitted from Contact Us Web Page:</b><br/><br/>");
            sbBody.AppendLine(String.Concat("Name: ", txtName.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Address: ", txtAddress.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Town: ", txtTown.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Post Code: ", txtPostCode.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Telephone: ", txtTel.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Email Address: ", txtEmailAddress.Text, "<br/>"));
            sbBody.AppendLine(String.Concat("Message: ", txtMessage.Text, "<br/><br/>"));
            sbBody.AppendLine("Thank You<br/>");
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