using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.Admin.Artists
{
    public partial class List : System.Web.UI.Page
    {
        private string status;

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
            cArtistType at = new cArtistType();
            DataView dv = new DataView();

            dv = at.List();
            if (dv.Table.Rows.Count > 0)
            {
                ListItem li1 = new ListItem();
                li1.Text = "--Please Select--";
                li1.Value = "";
                ddlArtistType.Items.Add(li1);
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = dv.Table.Rows[i]["Description"].ToString();
                    li.Value = dv.Table.Rows[i]["ArtistTypeID"].ToString();
                    ddlArtistType.Items.Add(li);
                }
            }
            DoSearch();
        }

        private void DoSearch(bool ignorePattern = false)
        {
            int ArtistTypeID = 0;
            int IsDeleted = -1;
            string Name = "";
            cArtist a = new cArtist();
           

            if (txtSearchName.Text.Length > 0)
            {
                Name = txtSearchName.Text.Trim().Replace("'", "''");
            }
            if (ddlArtistType.SelectedValue != null)
            {
                if (ddlArtistType.SelectedValue.Length > 0)
                {
                    ArtistTypeID = Convert.ToInt32(ddlArtistType.SelectedValue);
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

            string Pattern = "";

            if (!ignorePattern)
            {
                if (Request.QueryString["sAlpha"] != null)
                {
                    Pattern = Request.QueryString["sAlpha"].ToString();
                }
            }

            DataSet ds = a.Search(IsDeleted, Name, ArtistTypeID, Pattern);

            if (ds.Tables.Count > 0)
            {
                this.rptAlphabets.DataSource = ds.Tables[1];
                this.rptAlphabets.DataBind();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvArtists.DataSource = ds.Tables[0];
                gvArtists.DataBind();
                gvArtists.Visible = true;
                lblStatus.Visible = false;
            }
            else
            {
                gvArtists.Visible = false;
                lblStatus.Text = "There is no data available.";
                lblStatus.Visible = true;
            }
        }

        protected void gvArtists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArtists.PageIndex = e.NewPageIndex;
            DoSearch();
        }
        protected void gvArtists_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            cArtist art = new cArtist();
            cApprovals appr = new cApprovals();


            if (e.CommandName.Equals("deleteRecord"))
            {
                if (gvArtists.PageIndex > 0)
                { index = Convert.ToInt32(e.CommandArgument) - (gvArtists.PageIndex * gvArtists.PageSize); }
                else { index = Convert.ToInt32(e.CommandArgument); }

                int ArtistID = Convert.ToInt32(gvArtists.DataKeys[index]["ArtistID"]);

                string message = art.Delete(ArtistID, Session["FV_Username"].ToString());

                DoSearch();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup('" + message + "');", true);
            }
        }
        protected void gvArtists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ArtistID = 0;
            string ImageFileName = "";
            bool IsDeleted = false;
            cApprovals appr = new cApprovals();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ExhibitionNo = Convert.ToInt32(gvArtists.DataKeys[e.Row.RowIndex]["ExhibitionNo"]);
                ArtistID = Convert.ToInt32(gvArtists.DataKeys[e.Row.RowIndex]["ArtistID"]);
                IsDeleted = Convert.ToBoolean(gvArtists.DataKeys[e.Row.RowIndex]["IsDeleted"]);
                CheckBox chkExhibitionParticipation = (CheckBox)e.Row.FindControl("chkExhibitionParticipation");
                if (ExhibitionNo != 0)
                {
                    chkExhibitionParticipation.Checked = true;
                }
                else
                {
                    chkExhibitionParticipation.Checked = false;
                }
                if (gvArtists.DataKeys[e.Row.RowIndex]["ImageFileName"] != DBNull.Value)
                {
                    ImageFileName = gvArtists.DataKeys[e.Row.RowIndex]["ImageFileName"].ToString();
                }

                Image imgArtistPhoto = (Image)e.Row.FindControl("imgArtistPhoto");
                if (imgArtistPhoto != null)
                {
                    if (ImageFileName.Length > 0)
                    {
                        imgArtistPhoto.ImageUrl = "../../Uploads/Thumbnails/" + ImageFileName;
                        imgArtistPhoto.Visible = true;
                    }
                    else
                    { imgArtistPhoto.Visible = false; }
                }
                else
                {
                    imgArtistPhoto.Visible = false;
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
                    else if (status == "2")
                    {
                        lblApprovalStatus.Text = "Approved";
                        lblApprovalStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (status == "3")
                    {
                        lblApprovalStatus.Text = "Rejected";
                        lblApprovalStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                if (hypEdit != null)
                {
                    hypEdit.ToolTip = "Edit";
                    hypEdit.Style["Cursor"] = "hand";
                    hypEdit.Style["border"] = "0";
                    hypEdit.NavigateUrl = "Edit.aspx?ArtistID=" + ArtistID.ToString();
                }

                Button imgDel = (Button)e.Row.FindControl("butDelete");
                if (imgDel != null)
                {
                    string deleteMessage = "Are you sure you want to delete this item?";
                    imgDel.Style["Cursor"] = "hand";
                    if (IsDeleted == true)
                    {
                        deleteMessage = "Are you sure you want to undelete this item?";
                        imgDel.Text = "Undelete";
                    }
                    imgDel.Attributes.Add("onclick", "return ConfirmDelete('" + deleteMessage + "')");
                }

                HyperLink butPreview = (HyperLink)e.Row.FindControl("butPreview");
                if (butPreview != null)
                {
                    butPreview.NavigateUrl = "../Approvals/Preview.aspx?RetUrl=1&ArtistID=" + ArtistID.ToString();
                }
            }
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
            radArtistAll.Checked = false;
            radArtistDeleted.Checked = false;
            radArtistActive.Checked = true;
            txtSearchName.Text = "";
            DoSearch(true);
        }

        protected void ddlArtistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSearch();
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
                hlink.NavigateUrl = "/Admin/Artists/List.aspx?sAlpha=" + currentText;
            }
        }
    }
}