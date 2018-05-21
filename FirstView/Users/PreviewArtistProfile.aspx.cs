using System;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.PublicView
{
    public partial class PreviewArtistProfile : System.Web.UI.Page
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
            string ApprovalStatus = "";
            int ArtistID = 0;

            cArtist art = new cArtist();
            cArtistWorks aw = new cArtistWorks();
            cApprovals appr = new cApprovals();
            DataView dv = new DataView();
            DataView dv2 = new DataView();

            ArtistID = Convert.ToInt32(Context.Items["FV_ArtistID"]);

            dv = art.ListByID(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    lblFullname.Text = dv.Table.Rows[i]["Name"].ToString() + " " + dv.Table.Rows[i]["Surname"].ToString();
                    lblArtistType.Text = dv.Table.Rows[i]["ArtistType"].ToString();
                    if (dv.Table.Rows[i]["CV"] != DBNull.Value)
                    { lblCV.Text = dv.Table.Rows[i]["CV"].ToString(); }
                    if (dv.Table.Rows[i]["ImageFileName"] != DBNull.Value)
                    {
                        imgArtist.ImageUrl = "~/Uploads/Thumbnails/" + dv.Table.Rows[i]["ImageFileName"].ToString();
                        imgArtist.Visible = true;
                    }
                    else
                    { imgArtist.Visible = false; }
                }
            }

            // Check Approval Status 
            DataView dvAS = new DataView();
            int iAS = 0;
            string ApprovalComment = "";
            dvAS = appr.CheckProfileApprovalStatus(ArtistID);
            if (dvAS.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dvAS.Table.Rows.Count; i++)
                {
                    if (dvAS.Table.Rows[i]["ApprovalStatus"] != DBNull.Value)
                    {
                        iAS = Convert.ToInt32(dvAS.Table.Rows[i]["ApprovalStatus"]);
                    }
                    if (dvAS.Table.Rows[i]["ApprovalComment"] != DBNull.Value)
                    {
                        ApprovalComment = dvAS.Table.Rows[i]["ApprovalComment"].ToString();
                    }
                }
            }
            if (iAS == 1)
            {
                ApprovalStatus = "Submitted";
            }
            else if (iAS == 2)
            {
                ApprovalStatus = "Approved";
            }
            else if (iAS == 3)
            {
                ApprovalStatus = "Rejected";
            }

            if (ApprovalStatus == "")
            {
                butSubmitApprove.Visible = true;
                pGlyph.Style["display"] = "none";
            }
            else if (ApprovalStatus == "Submitted")
            {
                butSubmitApprove.Visible = false;
                txtApprovalComment.Visible = false;
                pGlyph.Attributes.Add("Class", "text-warning");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "Your profile has been submitted for approval.";
                lblApprovalStatus.Visible = true;
            }
            else if (ApprovalStatus == "Approved")
            {
                butSubmitApprove.Visible = true;
                pGlyph.Attributes.Add("Class", "text-success");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "Your profile has been approved.";
                lblApprovalStatus.Visible = true;
            }
            else if (ApprovalStatus == "Rejected")
            {
                butSubmitApprove.Visible = true;
                pGlyph.Attributes.Add("Class", "text-danger");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "Your profile has been rejected. Please update and resubmit";
                lblApprovalStatus.Visible = true;
            }

            butSubmitApprove.Attributes.Add("onclick", "return ConfirmSubmitApprove();");           
        }

        protected void butSubmitApprove_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView();
            cApprovals appr = new cApprovals();
            cUsers u = new cUsers();

            appr.UpdateArtistProfileStatus(Convert.ToInt32(Context.Items["FV_ArtistID"]), 1, txtApprovalComment.Text, 1);

            butSubmitApprove.Visible = false;
            pGlyph.Style["display"] = "";
            pGlyph.Attributes.Add("Class", "text-warning");
            lblApprovalStatus.Text = "Your page has been submitted for approval.";
            lblApprovalStatus.Visible = true;

            // Send Email to All Admins
            dv = u.ListAllAdmins();
            ArrayList alEmailIds = new ArrayList();
            string emailId = string.Empty;
            foreach (DataRow dr in dv.Table.Rows)
            {
                emailId = Convert.ToString(dr["Email"]);
                if (!alEmailIds.Contains(emailId))
                {
                    alEmailIds.Add(emailId);
                }
            }
            if (alEmailIds != null && alEmailIds.Count > 0)
            {
                for (int i = 0; i < alEmailIds.Count; i++)
                {
                    SendEmail(Convert.ToInt32(Context.Items["FV_ArtistID"]), alEmailIds[i].ToString());
                }
            }
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupApprove();", true);
        }
        private void SendEmail(int ArtistID, string EmailAddress)
        {
            string Surname = "";
            string Name = "";
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cArtist a = new cArtist();
            cSettings set = new cSettings();
            DataView dv = new DataView();

            dv = a.ListByID(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                }
            }

            Subject = "First-View Gallery Notification - Artist Profile - Submitted For Approval";

            SmtpClient smtpClient = new SmtpClient();
            FromEmailAddress = set.ListByCode("SystemEmailAddress");
            smtpClientHost = set.ListByCode("smtpClientHost");
            smtpClientPort = set.ListByCode("smtpClientPort");

            MailMessage message = new MailMessage();
            MailAddress mailFrom = new MailAddress(FromEmailAddress);
            message.To.Add(EmailAddress);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            sbBody.AppendLine("<b>Artist Profile - Submitted For Approval</b><br/><br/>");
            sbBody.AppendLine("Good day, <br/>");
            sbBody.AppendLine(String.Concat("The Artist: ", Name, " ", Surname, " has submitted their profile for approval. <br/>"));
            if (txtApprovalComment.Text.Length > 0)
            {
                sbBody.AppendLine(String.Concat("Comments: ", txtApprovalComment.Text, "<br/>"));
            }
            sbBody.AppendLine(String.Concat(@"<a href='http://first-view.uk/users/Login.aspx?RetUrl=1&ArtistID=", ArtistID.ToString(), "'> Click here to Approve/Reject this item.</a><br/><br/>"));
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