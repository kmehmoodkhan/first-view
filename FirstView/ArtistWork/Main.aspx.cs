using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.ArtistWork
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                LoadData();
                LoadGridView();
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
           
        }

        private void LoadGridView()
        {
            int ArtistID = 0;
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();

            ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);

            dv = aw.ListByArtistID(ArtistID, 0);
            if (dv.Table.Rows.Count > 0)
            {
                gvArtistWorks.DataSource = dv.Table;
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


            string commission = aw.GetArtistCommission(ArtistID);

            //if (!string.IsNullOrEmpty(commission) && commission != "0")
            //{
            //    gvArtistWorks.Columns[3].Visible = true;
            //    gvArtistWorks.Columns[4].Visible = false;
            //}
            //else
            //{
            //    gvArtistWorks.Columns[3].Visible = false;
            //    gvArtistWorks.Columns[4].Visible = true;
            //}
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", "showAddModal();", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add.aspx");
        }

        protected void gvArtistWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArtistWorks.PageIndex = e.NewPageIndex;
            LoadGridView();
        }
        protected void gvArtistWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            cArtistWorks aw = new cArtistWorks();
            cUsers u = new cUsers();
            DataView dv = new DataView();

            if (e.CommandName.Equals("deleteRecord"))
            {
                if (gvArtistWorks.PageIndex > 0)
                { index = Convert.ToInt32(e.CommandArgument) - (gvArtistWorks.PageIndex * gvArtistWorks.PageSize); }
                else { index = Convert.ToInt32(e.CommandArgument); }            

                int ArtistWorkID = Convert.ToInt32(gvArtistWorks.DataKeys[index]["ArtistWorkID"]);

                aw.Delete(ArtistWorkID, Session["FV_Username"].ToString());
                LoadGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
            }
        }
        protected void gvArtistWorks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ArtistID = 0;
            int ArtistWorkID = 0;
            string Width = "0";
            string Height = "0";
            string ImageFileName = "";
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ExhibitionNo = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ExhibitionNo"]);
                ArtistWorkID = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ArtistWorkID"]);
                ArtistID = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ArtistID"]);
                ExhibitionNo = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["ExhibitionNo"]);
                if (gvArtistWorks.DataKeys[e.Row.RowIndex]["ImageFileName"] != DBNull.Value)
                { ImageFileName = gvArtistWorks.DataKeys[e.Row.RowIndex]["ImageFileName"].ToString(); }
                if (gvArtistWorks.DataKeys[e.Row.RowIndex]["Width"] != DBNull.Value)
                { Width = Convert.ToString(gvArtistWorks.DataKeys[e.Row.RowIndex]["Width"]); }
                if (gvArtistWorks.DataKeys[e.Row.RowIndex]["Height"] != DBNull.Value)
                { Height = Convert.ToString(gvArtistWorks.DataKeys[e.Row.RowIndex]["Height"]); }



                Label lblSize = (Label)e.Row.FindControl("lblSize");
                Panel pnlExhibtion = (Panel)e.Row.FindControl("pnlExhibtion");
                Label lblExhibitionNo = (Label)e.Row.FindControl("lblExhibitionNo");

                if(ExhibitionNo!=0)
                {
                    pnlExhibtion.Visible = true;
                    lblExhibitionNo.Text = ExhibitionNo.ToString();
                }
                else
                {
                    pnlExhibtion.Visible = false;
                }

                if (lblSize != null)
                {
                    lblSize.Text =  Width.ToString() + "x" + Height.ToString() + "cms";
                    lblSize.Visible = true;
                }
                else
                {
                    lblSize.Visible = false;
                }
                Image imgArtistWork = (Image)e.Row.FindControl("imgArtistWork");
                if (imgArtistWork != null)
                {
                    if (ImageFileName.Length > 0)
                    {
                        imgArtistWork.ImageUrl = "../Uploads/Thumbnails/" + ImageFileName;
                        imgArtistWork.Visible = true;
                    }
                    else
                    { imgArtistWork.Visible = false; }
                }
                else
                {
                    imgArtistWork.Visible = false;
                }

                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                if (hypEdit != null)
                {
                    hypEdit.ToolTip = "Edit";
                    hypEdit.Style["Cursor"] = "hand";
                    hypEdit.Style["border"] = "0";
                    hypEdit.NavigateUrl = "Edit.aspx?ArtistWorkID=" + ArtistWorkID.ToString();
                }

                Button imgDel = (Button)e.Row.FindControl("butDelete");
                if (imgDel != null)
                {
                    imgDel.Attributes.Add("onclick", "return ConfirmDelete()");
                    imgDel.Style["Cursor"] = "hand";
                }
            }
        }

        protected void butPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Artist/Preview.aspx?RetUrl=2");
        }
    }
}