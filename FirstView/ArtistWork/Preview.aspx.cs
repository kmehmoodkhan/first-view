using FirstView.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstView.ArtistWork
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
                if (Convert.ToInt32(hdnfExhibitionNo.Value) > 0)
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

        protected void butBack_Click(object sender, EventArgs e)
        {
            string url = "/Admin/Approvals/List.aspx?Status=1";


            if (Request.QueryString["RetUrl"] != null)
            {
                if (Request.QueryString["RetUrl"].ToString() == "1")
                {
                    url = "~/Artists/List.aspx";
                }
                if (Request.QueryString["RetUrl"].ToString() == "2")
                {
                    url = "~/ArtistWork/List.aspx";
                }
            }

            Response.Redirect(url);
        }
    }
}