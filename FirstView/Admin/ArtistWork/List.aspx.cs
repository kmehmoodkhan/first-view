using System;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Common;

namespace FirstView.Admin.ArtistWork
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                ViewState["IgnorePattern"] = false;
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
            AddNew.Visible = false;
            LoadArtists();
            DoSearch();
        }

        private void LoadArtists()
        {
            cArtist a = new cArtist();
            DataView dv = new DataView();

            ddlArtist.Items.Clear();
             
            dv = a.List(0);
            if (dv.Table.Rows.Count > 0)
            {
                ListItem li1 = new ListItem();
                li1.Value = "";
                li1.Text = "--Select All--";
                ddlArtist.Items.Add(li1);
                for (int i =0; i < dv.Table.Rows.Count;i++)
                {
                    ListItem li2 = new ListItem();
                    li2.Value = dv.Table.Rows[i]["ArtistID"].ToString();
                    li2.Text = dv.Table.Rows[i]["Name"].ToString() + " " + dv.Table.Rows[i]["Surname"].ToString();
                    ddlArtist.Items.Add(li2);
                }

                if (Request.QueryString["ArtistID"] != null)
                {
                    ddlArtist.SelectedValue = Request.QueryString["ArtistID"].ToString();
                }
            }
        }

        private void DoSearch(bool ignorePattern = false)
        {
            int IsDeleted = -1;
            int ArtistID = 0;
            string WorkName = "";
            string Note = "";
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();

            AddNew.Visible = false;
            if (ddlArtist.SelectedItem != null)
            {
                if (ddlArtist.SelectedItem.Value.Length > 0)
                {
                    ArtistID = Convert.ToInt32(ddlArtist.SelectedItem.Value);
                    AddNew.HRef = "Add.aspx?ArtistID=" + ArtistID.ToString();
                    AddNew.Visible = true;
                }
            }

            if (radArtistAll.Checked == true)
            {
                IsDeleted = -1;
            }
            else if (radArtistActive.Checked == true)
            {
                IsDeleted = 0;
            }
            else if (radArtistDeleted.Checked == true)
            {
                IsDeleted = 1;
            }

            if (txtSearchWorkName.Text.Length > 0)
            { WorkName = txtSearchWorkName.Text; }
            if (txtSearchNote.Text.Length > 0)
            { Note = txtSearchNote.Text; }

            string Pattern = "";

            if (!ignorePattern)
            {
                if (Request.QueryString["sAlpha"] != null)
                {
                    Pattern = Request.QueryString["sAlpha"].ToString();
                }
            }

            DataSet ds = aw.Search(ArtistID, IsDeleted, WorkName, Note, Pattern);

            if (ds.Tables.Count > 0)
            {
                this.rptAlphabets.DataSource = ds.Tables[1];
                this.rptAlphabets.DataBind();
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvArtistWorks.DataSource = ds.Tables[0];
                gvArtistWorks.DataBind();
                gvArtistWorks.Visible = true;
                lblStatus.Visible = false;                
            }
            else
            {
                gvArtistWorks.Visible = false;
                lblStatus.Text = "There is no data available.";
                lblStatus.Visible = true;
            }
        }

        protected void gvArtistWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArtistWorks.PageIndex = e.NewPageIndex;
            DoSearch();
        }
        protected void gvArtistWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            cArtistWorks aw = new cArtistWorks();
            cApprovals appr = new cApprovals();

            if (e.CommandName.Equals("deleteRecord"))
            {
                if (gvArtistWorks.PageIndex > 0)
                { index = Convert.ToInt32(e.CommandArgument) - (gvArtistWorks.PageIndex * gvArtistWorks.PageSize); }
                else { index = Convert.ToInt32(e.CommandArgument); }

                int ArtistWorkID = Convert.ToInt32(gvArtistWorks.DataKeys[index]["ArtistWorkID"]);

                string msg=aw.Delete(ArtistWorkID, Session["FV_Username"].ToString());
                DoSearch();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup('"+ msg + "');", true);
            }
        }
        string status = "0";
        protected void gvArtistWorks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ArtistID = 0;
            int ArtistWorkID = 0;
            string ImageFileName = "";
            bool IsDeleted = false;
            string ApprovalStatus = "";
            bool IsAllowed = false;
            cApprovals appr = new cApprovals();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ExhibitionNo = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ExhibitionNo"]);
                ArtistWorkID = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ArtistWorkID"]);
                ArtistID = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ArtistID"]);
                IsDeleted = Convert.ToBoolean(gvArtistWorks.DataKeys[e.Row.RowIndex]["IsDeleted"]);
                IsAllowed = Convert.ToBoolean(gvArtistWorks.DataKeys[e.Row.RowIndex]["IsAllowed"]);
                CheckBox chkExhibitionParticipation = (CheckBox)e.Row.FindControl("chkExhibitionParticipation");
                if (ExhibitionNo != 0)
                {
                    chkExhibitionParticipation.Checked = true;
                }
                else
                {
                    chkExhibitionParticipation.Checked = false;
                }
                chkExhibitionParticipation.Visible = IsAllowed;
                if (gvArtistWorks.DataKeys[e.Row.RowIndex]["ImageFileName"] != DBNull.Value)
                { ImageFileName = gvArtistWorks.DataKeys[e.Row.RowIndex]["ImageFileName"].ToString(); }

                Image imgArtistWork = (Image)e.Row.FindControl("imgArtistWork");
                if (imgArtistWork != null)
                {
                    if (ImageFileName.Length > 0)
                    {
                        imgArtistWork.ImageUrl = "../../Uploads/Thumbnails/" + ImageFileName;
                        imgArtistWork.Visible = true;
                    }
                    else
                    { imgArtistWork.Visible = false; }
                }
                else
                {
                    imgArtistWork.Visible = false;
                }

                Label lblIsDeleted = (Label)e.Row.FindControl("lblIsDeleted");
                if (lblIsDeleted != null)
                {
                    if (IsDeleted == true)
                    {
                        lblIsDeleted.Text = "Yes";
                    }
                    else if (IsDeleted == false)
                    {
                        lblIsDeleted.Text = "No";
                    }
                }
                if (IsDeleted == true)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB9B9");
                }

                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                if (hypEdit != null)
                {
                    hypEdit.ToolTip = "Edit";
                    hypEdit.Style["Cursor"] = "hand";
                    hypEdit.Style["border"] = "0";
                    hypEdit.NavigateUrl = "Edit.aspx?ArtistID=" + ArtistID.ToString() + "&ArtistWorkID=" + ArtistWorkID.ToString();
                }

                //Button imgDel = (Button)e.Row.FindControl("butDelete");
                //if (imgDel != null)
                //{
                //    string deleteMessage = "Are you sure you want to delete this item?";
                //    imgDel.Style["Cursor"] = "hand";
                //    if (IsDeleted == true)
                //    {
                //        deleteMessage = "Are you sure you want to undelete this item?";
                //        imgDel.Text = "Undelete";
                //    }
                //    imgDel.Attributes.Add("onclick", "return ConfirmDelete('" + deleteMessage + "')");
                //}

                // Check Approval Status 
                ApprovalStatus = "";//appr.CheckArtistWorkStatus(ArtistWorkID);
                Label lblApprovalStatus = (Label)e.Row.FindControl("lblApprovalStatus");
                HiddenField hdnfApprovalStatus = (HiddenField)e.Row.FindControl("hdnfApprovalStatus");
                if (hdnfApprovalStatus != null)
                {
                    status = hdnfApprovalStatus.Value;
                    
                    if (status == "1")
                    {
                        lblApprovalStatus.ForeColor = System.Drawing.Color.Orange;
                        lblApprovalStatus.Text = "Submitted";
                    }
                    else if (status=="2")
                    {
                        lblApprovalStatus.Text = "Approved";
                        lblApprovalStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (status=="3")
                    {
                        lblApprovalStatus.Text = "Rejected";
                        lblApprovalStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }

                Button butApprove = (Button)e.Row.FindControl("butApprove");
                if (butApprove != null)
                {
                    butApprove.Attributes.Add("onclick", "return ConfirmApprove()");
                    if (ApprovalStatus == "" || ApprovalStatus == "Submitted")
                    { butApprove.Visible = true; }
                    else { butApprove.Visible = false; }
                }
                Button butReject = (Button)e.Row.FindControl("butReject");
                if (butReject != null)
                {
                    butReject.Attributes.Add("onclick", "return ConfirmReject()");
                    if (ApprovalStatus == "" || ApprovalStatus == "Submitted")
                    { butReject.Visible = true; }
                    else { butReject.Visible = false; }
                }
                Button butReset = (Button)e.Row.FindControl("butReset");
                if (butReset != null)
                {
                    butReset.Attributes.Add("onclick", "return ConfirmReset()");
                    if (ApprovalStatus == "Approved" || ApprovalStatus == "Rejected")
                    { butReset.Visible = true; }
                    else { butReset.Visible = false; }
                }
                HyperLink butPreview = (HyperLink)e.Row.FindControl("butPreview");
                if (butPreview != null)
                {
                    butPreview.NavigateUrl = "../Approvals/Preview.aspx?RetUrl=2&ArtistID=" + ArtistID.ToString();
                }
            }
        }

        protected void ddlArtist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void radArtistAll_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void radArtistActive_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void radArtistDeleted_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            ViewState["IgnorePattern"] = true;
            radArtistActive.Checked = true;
            radArtistAll.Checked = false;
            radArtistDeleted.Checked = false;
            ddlArtist.SelectedValue = "";
            txtSearchNote.Text = "";
            txtSearchWorkName.Text = "";
            DoSearch(true);
        }

        private void SendEmail(int ArtistWorkID, int ApprovalStatus)
        {
            string Surname = "";
            string Name = "";
            string EmailAddress = "";
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "";
            string WorkName = "";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();
            cSettings set = new cSettings();

            dv = aw.ListByIDForEmail(ArtistWorkID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                    if (dv.Table.Rows[i]["Email"] != DBNull.Value)
                    { EmailAddress = dv.Table.Rows[i]["Email"].ToString(); }
                    if (dv.Table.Rows[i]["WorkName"] != DBNull.Value)
                    { WorkName = dv.Table.Rows[i]["WorkName"].ToString(); }
                }
            }

            if (ApprovalStatus == 2)
            { Subject = "First-View Gallery Notification - Artist Work - Approved"; }
            else if (ApprovalStatus == 3)
            { Subject = "First-View Gallery Notification - Artist Work - Rejected"; }
            else if (ApprovalStatus == 4)
            { Subject = "First-View Gallery Notification - Artist Work - Reset"; }

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
                sbBody.AppendLine("<b>Artist Work - Approved</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " " + Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your Work: '<b>" , WorkName , "</b>' has been Approved.<br/>"));                
            }
            else if (ApprovalStatus == 3)
            {
                sbBody.AppendLine("<b>Artist Work - Rejected</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " " + Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your Work: '<b>", WorkName, "</b>' has been Rejected.<br/>"));
            }
            else if (ApprovalStatus == 4)
            {
                sbBody.AppendLine("<b>Artist Work - Reset</b><br/><br/>");
                sbBody.AppendLine(String.Concat("Hello ", Name, " " + Surname, ", <br/>"));
                sbBody.AppendLine(String.Concat("Your Work: '<b>", WorkName, "</b>' has been Reset. You can now update/modify this item and re-submit for approval.<br/>"));
            }
            sbBody.AppendLine(String.Concat("<a href='http://first-view.uk/users/Login.aspx?RetUrl=2&ArtistWorkID=", ArtistWorkID.ToString(), "'> Click here to login.</a><br/><br/>"));
            sbBody.AppendLine("Thank You<br/>");
            sbBody.AppendLine("Admin @ First-View");

            message.Subject = Subject;
            message.Body = sbBody.ToString();

            smtpClient.Host = smtpClientHost;
            smtpClient.Port = Convert.ToInt32(smtpClientPort);

            smtpClient.Send(message);
            message.Dispose();
        }

        protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void btnExhibition_Click(object sender, EventArgs e)
        {

        }

        private string Pattern
        {
            get
            {
                string Pattern = "";

                if (Request.QueryString["sAlpha"] != null)
                {
                    Pattern = Request.QueryString["sAlpha"].ToString();
                }
                return Pattern;
            }
        }

        protected void rptAlphabets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hlink = e.Item.FindControl("linkIndex") as HyperLink;
                string currentText = ((System.Data.DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString();

                bool ignorePattern = false;
                if (ViewState["IgnorePattern"] != null)
                {
                    ignorePattern = Convert.ToBoolean(ViewState["IgnorePattern"]);
                }
                if (currentText.ToLower().Equals(Pattern.ToLower()) && !ignorePattern)
                {

                    hlink.CssClass += " selected";
                }
                hlink.NavigateUrl = "/Admin/ArtistWork/List.aspx?sAlpha=" + currentText;
            }
        }
    }
}