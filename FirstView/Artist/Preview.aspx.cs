using System;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Artist
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
                Response.Redirect("../Users/Login.aspx?im=3");
            }
            if (Session["FV_UserID"].ToString().Length == 0)
            {
                Response.Redirect("../Users/Login.aspx?im=3");
            }
        }

        private void LoadData()
        {
            try
            {
                string ApprovalStatus = "";
                int ArtistID = 0;

                cArtist art = new cArtist();
                cArtistWorks aw = new cArtistWorks();
                cApprovals appr = new cApprovals();
                DataView dv = new DataView();
                DataView dv2 = new DataView();

                ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);

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
                    ArtistWork.DataSource = dv2.Table.Select("ArtistId<>0").CopyToDataTable();
                    ArtistWork.DataBind();
                }

                // Check Approval Status 
                DataView dvAS = new DataView();

                dvAS = appr.CheckWorkApprovalStatus(ArtistID);

                var dtPendingRows = dv2.ToTable().Select("ApprovalStatus is null");

                if (dtPendingRows!=null && dtPendingRows.Length> 0)
                {
                    ApprovalStatus = "Pending";

                }
                else
                {
                    dv2.Sort = "ApprovedDate DESC";
                    DataTable dt = dv2.ToTable();
                    ApprovalStatus = Convert.ToString(dt.Rows[0]["IsApprovalText"]);

                }
                if (ApprovalStatus == "Pending" || ApprovalStatus == string.Empty)
                {
                    butSubmitApprove.Visible = true;
                    pGlyph.Attributes.Add("Class", "text-warning");
                    pGlyph.Style["display"] = "";
                    lblApprovalStatus.Text = "Pending for submission.";
                    lblApprovalStatus.Visible = true;
                }
                else if (ApprovalStatus == "Submitted")
                {
                    butSubmitApprove.Visible = false;
                    txtApprovalComment.Visible = false;
                    pGlyph.Attributes.Add("Class", "text-warning");
                    pGlyph.Style["display"] = "";
                    lblApprovalStatus.Text = "Your page has been submitted for approval.";
                    lblApprovalStatus.Visible = true;
                }
                else if (ApprovalStatus == "Approved")
                {
                    butSubmitApprove.Visible = false;
                    txtApprovalComment.Visible = false;
                    pGlyph.Attributes.Add("Class", "text-success");
                    pGlyph.Style["display"] = "";
                    lblApprovalStatus.Text = "Your page has been approved.";
                    lblApprovalStatus.Visible = true;
                }
                else if (ApprovalStatus == "Rejected")
                {
                    butSubmitApprove.Visible = true;
                    txtApprovalComment.Visible = true;
                    pGlyph.Attributes.Add("Class", "text-danger");
                    pGlyph.Style["display"] = "";
                    lblApprovalStatus.Text = "Your page has been rejected. Please update and resubmit";
                    lblApprovalStatus.Visible = true;
                }

                butSubmitApprove.Attributes.Add("onclick", "return ConfirmSubmitApprove();");

                butBack.HRef = "Menu.aspx";
                if (Request.QueryString["RetUrl"] != null)
                {
                    if (Request.QueryString["RetUrl"] == "1")
                    { butBack.HRef = "ProfileEdit.aspx"; }
                    if (Request.QueryString["RetUrl"] == "2")
                    { butBack.HRef = "../ArtistWork/Main.aspx"; }
                }
            }
            catch (Exception ex)
            {
                cSettings.LogException(ex);
            }
        }

        protected void butSubmitApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView();
                cApprovals appr = new cApprovals();
                cUsers u = new cUsers();
                string artistWorkIds = string.Empty;
                foreach (var item in ArtistWork.Items)
                {
                    HiddenField hdnfArtistWorkId = item.FindControl("hdnfArtistWorkID") as HiddenField;
                    artistWorkIds = artistWorkIds + (artistWorkIds.Length > 0 ? "," : "") + hdnfArtistWorkId.Value.Trim();
                }

                appr.UpdateArtistWorkStatus(artistWorkIds, Convert.ToInt32(Session["FV_UserID"]), txtApprovalComment.Text, 1);

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
                        SendEmail(Convert.ToInt32(Session["FV_ArtistID"]), alEmailIds[i].ToString());
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupApprove();", true);
            }
            catch (Exception ex)
            {
                cSettings.LogException(ex);
            }
        }

        private void SendEmail(int ArtistID, string EmailAddress)
        {
            try
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

                Subject = "First-View Gallery Notification - Artist Work - Submitted For Approval";

                SmtpClient smtpClient = new SmtpClient();
                FromEmailAddress = set.ListByCode("SystemEmailAddress");
                smtpClientHost = set.ListByCode("smtpClientHost");
                smtpClientPort = set.ListByCode("smtpClientPort");

                MailMessage message = new MailMessage();
                MailAddress mailFrom = new MailAddress(FromEmailAddress);
                message.To.Add(EmailAddress);
                message.IsBodyHtml = true;
                message.From = mailFrom;

                sbBody.AppendLine("<b>Artist Work - Submitted For Approval</b><br/><br/>");
                sbBody.AppendLine("Good day, <br/>");
                sbBody.AppendLine(String.Concat("The Artist: ", Name, " ", Surname, " has submitted their page for approval. <br/>"));
                if (txtApprovalComment.Text.Length > 0)
                { sbBody.AppendLine(String.Concat("Comments: ", txtApprovalComment.Text, "<br/>")); }
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
            catch (Exception ex)
            {
                cSettings.LogException(ex);
            }
        }

        protected void ArtistWork_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                Label lblExhibitionNo = null;
                Panel pnlExhibition = null;
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    pnlExhibition = e.Item.FindControl("pnlExhibition") as Panel;
                    lblExhibitionNo = e.Item.FindControl("lblExhibitionNo") as Label;
                    if (lblExhibitionNo.Text != "0" && !string.IsNullOrEmpty((lblExhibitionNo.Text.Trim())))
                    {
                        pnlExhibition.Visible = true;
                    }
                    else
                    {
                        pnlExhibition.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                cSettings.LogException(ex);
            }

        }
    }
}