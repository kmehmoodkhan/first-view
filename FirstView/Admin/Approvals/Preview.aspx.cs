using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Admin.Approvals
{
    public partial class Preview : System.Web.UI.Page
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
            string ApprovalStatus = "";
            int ArtistID = 0;

            cArtist art = new cArtist();
            cArtistWorks aw = new cArtistWorks();
            cApprovals appr = new cApprovals();
            DataView dv = new DataView();
            DataView dv2 = new DataView();

            if (Request.QueryString["ArtistID"] != null)
            { ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]); }

            dv = art.ListByID(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    lblFullname.Text = dv.Table.Rows[i]["Name"].ToString() + " " + dv.Table.Rows[i]["Surname"].ToString();
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

            dv2 = aw.ListByArtistIDForPreview(ArtistID, 0);
            if (dv2.Table.Rows.Count > 0)
            {
                ArtistWork.DataSource = dv2.Table;
                ArtistWork.DataBind();
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
            var pendingRows = dv2.ToTable().Select("ApprovalStatus<>2");
            if (pendingRows.Length>0)
            {
                iAS = 1;
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
                pGlyph.Style["display"] = "none";
            }
            else if (ApprovalStatus == "Submitted")
            {
                pGlyph.Attributes.Add("Class", "text-warning");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "The artist page has been submitted for approval.";
                if (ApprovalComment.Length > 0)
                { lblApprovalStatus.Text = lblApprovalStatus.Text + " " + ApprovalComment; }
                lblApprovalStatus.Visible = true;
            }
            else if (ApprovalStatus == "Approved")
            {
                pGlyph.Attributes.Add("Class", "text-success");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "The artist page has been approved.";
                if (ApprovalComment.Length > 0)
                { lblApprovalStatus.Text = lblApprovalStatus.Text + " " + ApprovalComment; }
                lblApprovalStatus.Visible = true;
            }
            else if (ApprovalStatus == "Rejected")
            {
                pGlyph.Attributes.Add("Class", "text-danger");
                pGlyph.Style["display"] = "";
                lblApprovalStatus.Text = "The artist page has been rejected.";
                if (ApprovalComment.Length > 0)
                { lblApprovalStatus.Text = lblApprovalStatus.Text + " " + ApprovalComment; }
                lblApprovalStatus.Visible = true;
            }

            butApprove.Attributes.Add("onclick", "return ConfirmApprove();");
            butReject.Attributes.Add("onclick", "return ConfirmReject();");
            butReset.Attributes.Add("onclick", "return ConfirmReset();");

            if (Request.QueryString["RetUrl"] != null)
            {
                if (Request.QueryString["RetUrl"].ToString() == "1")
                {
                    butBack.HRef = "../Artists/List.aspx";
                }
                if (Request.QueryString["RetUrl"].ToString() == "2")
                {
                    butBack.HRef = "../ArtistWork/List.aspx";
                }
            }
        }

        private void SendEmail(int ArtistID, int ApprovalStatus)
        {
            string Surname = "";
            string Name = "";
            string EmailAddress = "";
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cArtist a = new cArtist();
            DataView dv = new DataView();
            cSettings set = new cSettings();

            dv = a.ListByIDForEmail(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    {
                        Surname = dv.Table.Rows[i]["Surname"].ToString();
                    }
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    {
                        Name = dv.Table.Rows[i]["Name"].ToString();
                    }
                    if (dv.Table.Rows[i]["Email"] != DBNull.Value)
                    {
                        EmailAddress = dv.Table.Rows[i]["Email"].ToString();
                    }
                }
            }

            if (ApprovalStatus == 2)
            {
                Subject = "First-View Gallery Notification - Artist Page - Approved";
            }
            else if (ApprovalStatus == 3)
            {
                Subject = "First-View Gallery Notification - Artist Page - Rejected";
            }
            else if (ApprovalStatus == 4)
            {
                Subject = "First-View Gallery Notification - Artist Page - Reset";
            }

            SmtpClient smtpClient = new SmtpClient();
            FromEmailAddress = set.ListByCode("SystemEmailAddress");
            smtpClientHost = set.ListByCode("smtpClientHost");
            smtpClientPort = set.ListByCode("smtpClientPort");

            MailMessage message = new MailMessage();
            MailAddress mailFrom = new MailAddress(FromEmailAddress);
            message.To.Add(EmailAddress);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            if (ApprovalStatus == 2)
            {
                sbBody.AppendLine("<b>Artist Page - Approved</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " ", Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your page has been approved.<br/>"));
            }
            else if (ApprovalStatus == 3)
            {
                sbBody.AppendLine("<b>Artist Work - Rejected</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " ", Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your page has been rejected.<br/>"));
            }
            else if (ApprovalStatus == 4)
            {
                sbBody.AppendLine("<b>Artist Work - Reset</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " ", Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your page has been reset. You can now update/modify your page and re-submit for approval.<br/>"));
            }
            if (txtApprovalComment.Text.Length > 0)
            {
                sbBody.AppendLine(String.Concat("Comments:", txtApprovalComment.Text, "<br/>"));
            }
            sbBody.AppendLine(String.Concat("<a href='http://first-view.uk/users/Login.aspx'> Click here to login.</a><br/><br/>"));
            sbBody.AppendLine("Thank You<br/>");
            sbBody.AppendLine("Admin @ First-View");

            message.Subject = Subject;
            message.Body = sbBody.ToString();

            smtpClient.Host = smtpClientHost;
            smtpClient.Port = Convert.ToInt32(smtpClientPort);

            smtpClient.Send(message);
            message.Dispose();
        }

        protected void butApprove_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }
            string artistWorkIds = string.Empty;
            foreach (var item in ArtistWork.Items)
            {
                CheckBox chkWork = item.FindControl("chkWork") as CheckBox;
                if (chkWork.Checked)
                {
                    HiddenField hdnfArtistWorkId = item.FindControl("hdnfArtistWorkId") as HiddenField;
                    artistWorkIds = artistWorkIds + (artistWorkIds.Length > 0 ? "," : "") + hdnfArtistWorkId.Value.Trim();
                }

            }
            cApprovals appr = new cApprovals();
            appr.UpdateArtistWorkStatus(artistWorkIds, Convert.ToInt32(Session["FV_UserID"]), txtApprovalComment.Text, 2);

            pGlyph.Style["display"] = "";
            pGlyph.Attributes.Add("Class", "text-success");
            lblApprovalStatus.Text = "The Artist Page has been Approved.";
            lblApprovalStatus.Visible = true;
            SendEmail(ArtistID, 2);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupApprove();", true);
        }

        protected void butReject_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;

            if (Request.QueryString["ArtistID"] != null)
            { ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]); }
            string artistWorkIds = string.Empty;
            foreach (var item in ArtistWork.Items)
            {
                CheckBox chkWork = item.FindControl("chkWork") as CheckBox;
                if (chkWork.Checked)
                {
                    HiddenField hdnfArtistWorkId = item.FindControl("hdnfArtistWorkId") as HiddenField;
                    artistWorkIds = artistWorkIds + (artistWorkIds.Length > 0 ? "," : "") + hdnfArtistWorkId.Value.Trim();
                }

            }
            cApprovals appr = new cApprovals();
            appr.UpdateArtistWorkStatus(artistWorkIds, Convert.ToInt32(Session["FV_UserID"]), txtApprovalComment.Text, 3);

            butApprove.Visible = false;
            butReject.Visible = false;
            butReset.Visible = true;
            pGlyph.Style["display"] = "";
            pGlyph.Attributes.Add("Class", "text-danger");
            lblApprovalStatus.Text = "The Artist Page has been Rejected.";
            lblApprovalStatus.Visible = true;

            SendEmail(ArtistID, 3);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "ShowPopupReject();", true);
        }

        protected void butReset_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;
            string artistWorkIds = string.Empty;
            if (Request.QueryString["ArtistID"] != null)
            { ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]); }

            foreach (var item in ArtistWork.Items)
            {
                HiddenField hdnfArtistWorkId = item.FindControl("hdnfArtistWorkId") as HiddenField;
                artistWorkIds = artistWorkIds + (artistWorkIds.Length > 0 ? "," : "") + hdnfArtistWorkId.Value.Trim();
            }
            cApprovals appr = new cApprovals();
            appr.UpdateArtistWorkStatus(artistWorkIds, Convert.ToInt32(Session["FV_UserID"]), txtApprovalComment.Text, null);

            butApprove.Visible = true;
            butReject.Visible = true;
            butReset.Visible = false;
            pGlyph.Style["display"] = "none";

            SendEmail(ArtistID, 4);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "ShowPopupReset();", true);
        }

        protected void ArtistWork_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            CheckBox chkWork;
            HiddenField hdnfIsApproved;
            HiddenField hdnfExhibitionNo;
            CheckBox chkExhibitionParticipation;
            bool IsAllowed = false;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                IsAllowed = Convert.ToBoolean(ArtistWork.DataKeys[e.Item.DataItemIndex]["IsAllowed"]);
                // Display the e-mail address in italics.
                hdnfExhibitionNo = (HiddenField)e.Item.FindControl("hdnfExhibitionNo");
                chkWork = (CheckBox)e.Item.FindControl("chkWork");
                chkExhibitionParticipation = (CheckBox)e.Item.FindControl("chkExhibitionParticipation");
                if(Convert.ToInt32(hdnfExhibitionNo.Value)>0)
                {
                    chkExhibitionParticipation.Checked = true;
                }
                else
                {
                    chkExhibitionParticipation.Checked = false;
                }
                chkExhibitionParticipation.Visible = IsAllowed;
                hdnfIsApproved = (HiddenField)e.Item.FindControl("hdnfIsApproved");
                if (hdnfIsApproved.Value == "1")
                {
                    chkWork.Checked = false;
                }
                else if (hdnfIsApproved.Value == "3")
                {
                    chkWork.Checked = false;
                }
                else if (hdnfIsApproved.Value == "2")
                {
                    chkWork.Checked = true;
                }
                else
                {
                    chkWork.Checked = false;
                }
            }
        }
    }
}