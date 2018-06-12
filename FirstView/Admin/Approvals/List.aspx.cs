﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Common;
namespace FirstView.Admin.Approvals
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                LoadData();
                ViewState["IgnorePattern"] = false;
            }
        }

        private int Status
        {
            get
            {
                int Status = 0;
                if(Request.QueryString["Status"] != null)
                {
                    Status = Convert.ToInt32(Request.QueryString["Status"]);
                }
                return Status;
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

        private void LoadData()
        {
            // Load Approval Status
            ListItem li1 = new ListItem();
            li1.Text = "--Select All--";
            li1.Value = "0";
            ddlApprovalStatus.Items.Add(li1);
            ListItem li2 = new ListItem();
            li2.Text = "Submitted For Approval";
            li2.Value = "1";
            ddlApprovalStatus.Items.Add(li2);
            ListItem li3 = new ListItem();
            li3.Text = "Approved";
            li3.Value = "2";
            ddlApprovalStatus.Items.Add(li3);
            ListItem li4 = new ListItem();
            li4.Text = "Rejected";
            li4.Value = "3";
            ddlApprovalStatus.Items.Add(li4);

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
                for (int i = 0; i < dv.Table.Rows.Count; i++)
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
            int ApprovalStatus = 0;
            int ArtistID = 0;
            
            cApprovals appr = new cApprovals();

            if (ddlArtist.SelectedItem != null)
            {
                if (ddlArtist.SelectedItem.Value.Length > 0)
                {
                    ArtistID = Convert.ToInt32(ddlArtist.SelectedItem.Value);
                }
            }

         
            if(Status > 0 && ViewState["StatusUsed"]== null)
            {
                ddlApprovalStatus.SelectedValue = Status.ToString();
                ViewState["StatusUsed"] = true;
            }

            if (ddlApprovalStatus.SelectedValue != null)
            {
                if (ddlApprovalStatus.SelectedValue.Length > 0)
                {
                    ApprovalStatus = Convert.ToInt32(ddlApprovalStatus.SelectedValue);
                }
            }


            int DeleteStatus = 0;
            if(radArtistAll.Checked)
            {
                DeleteStatus = 2;
            }
            else if(radArtistActive.Checked)
            {
                DeleteStatus = 0;
            }
            else
            {
                DeleteStatus = 1;
            }

            string Pattern = "";

            if (!ignorePattern)
            {
                if (Request.QueryString["sAlpha"] != null)
                {
                    Pattern = Request.QueryString["sAlpha"].ToString();
                }
            }
                

            DataSet ds = appr.Search(ArtistID, ApprovalStatus,Pattern, DeleteStatus);



            if (ds.Tables.Count > 0)
            {
                rptAlphabets.DataSource = ds.Tables[0];
                rptAlphabets.DataBind();

                gvApprovals.DataSource = ds.Tables[1];
                gvApprovals.DataBind();
                gvApprovals.Visible = true;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblStatus.Visible = false;
                }
                else
                {
                    lblStatus.Visible = true;
                    lblStatus.Text = "There is no data available.";
                }
            }
            else
            {
                gvApprovals.Visible = false;
                lblStatus.Text = "There is no data available.";
                lblStatus.Visible = true;
                rptAlphabets.Visible = false;
            }
        }

        protected void gvApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApprovals.PageIndex = e.NewPageIndex;
            DoSearch();
        }
        protected void gvApprovals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ArtistID = 0;
            int ApprovalStatus = 0;
            string strDate = "";
            string strApprovalStatus = "";

            cApprovals appr = new cApprovals();
            CommonLibrary cl = new CommonLibrary();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArtistID = Convert.ToInt32(gvApprovals.DataKeys[e.Row.RowIndex]["ArtistID"]);

                if (gvApprovals.DataKeys[e.Row.RowIndex]["ApprovalStatus"] != DBNull.Value)
                { ApprovalStatus = Convert.ToInt32(gvApprovals.DataKeys[e.Row.RowIndex]["ApprovalStatus"]); }

                if (gvApprovals.DataKeys[e.Row.RowIndex]["ApprovedDate"] != DBNull.Value)
                { strDate = cl.FormatDateTime(Convert.ToDateTime(gvApprovals.DataKeys[e.Row.RowIndex]["ApprovedDate"])); }

                if (ApprovalStatus == 1)
                { strApprovalStatus = "Submitted"; }
                else if (ApprovalStatus == 2)
                { strApprovalStatus = "Approved"; }
                if (ApprovalStatus == 3)
                { strApprovalStatus = "Rejected"; }

                Label lblDate = (Label)e.Row.FindControl("lblDate");
                if (lblDate != null)
                {
                    lblDate.Text = strDate;
                }
                Label lblApproved = (Label)e.Row.FindControl("lblApproved");
                if (lblApproved != null)
                {
                    lblApproved.Text = strApprovalStatus;
                    if (ApprovalStatus == 1)
                    { lblApproved.ForeColor = System.Drawing.Color.Orange; }
                    else if (ApprovalStatus == 2)
                    { lblApproved.ForeColor = System.Drawing.Color.Green; }
                    else if (ApprovalStatus == 3)
                    { lblApproved.ForeColor = System.Drawing.Color.Red; }
                }
                HyperLink butPreview = (HyperLink)e.Row.FindControl("butPreview");
                if (butPreview != null)
                {
                    butPreview.NavigateUrl = "Preview.aspx?ArtistID=" + ArtistID.ToString();
                }
            }
        }

        protected void ddlArtist_SelectedIndexChanged(object sender, EventArgs e)
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
            ddlArtist.SelectedValue = "";
            ddlApprovalStatus.SelectedValue = "0";
            DoSearch(true);
        }
        protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
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
                hlink.NavigateUrl = "/Admin/Approvals/List.aspx?sAlpha=" + currentText;
            }
        }
    }
}