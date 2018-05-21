using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.Admin.ArtistType
{
    public partial class List : System.Web.UI.Page
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
            DoSearch();
        }       

        private void DoSearch()
        {
            string ArtistType = "";
            cArtistType at = new cArtistType();
            DataView dv = new DataView();

            if (txtSearchArtistType.Text.Length > 0)
            {
                ArtistType = txtSearchArtistType.Text;
            }

            dv = at.Search(ArtistType);

            if (dv.Table.Rows.Count > 0)
            {   
                gvArtistTypes.DataSource = dv.Table;
                gvArtistTypes.DataBind();
                gvArtistTypes.Visible = true;
                lblStatus.Visible = false;
            }
            else
            {
                gvArtistTypes.Visible = false;
                lblStatus.Text = "There is no data available.";
                lblStatus.Visible = true;
            }
        }

        protected void gvArtistTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArtistTypes.PageIndex = e.NewPageIndex;
            DoSearch();
        }
        protected void gvArtistTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            bool CanDelete = false;
            cArtistType at = new cArtistType();

            if (e.CommandName.Equals("deleteRecord"))
            {
                if (gvArtistTypes.PageIndex > 0)
                { index = Convert.ToInt32(e.CommandArgument) - (gvArtistTypes.PageIndex * gvArtistTypes.PageSize); }
                else { index = Convert.ToInt32(e.CommandArgument); }

                int ArtistTypeID = Convert.ToInt32(gvArtistTypes.DataKeys[index]["ArtistTypeID"]);
                CanDelete = at.CanDelete(ArtistTypeID);
                if (CanDelete == true)
                {
                    at.Delete(ArtistTypeID);
                    DoSearch();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDeleteSuccess();", true);
                }
                else
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDeleteError();", true); }
            }
        }
        protected void gvArtistTypes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ArtistTypeID = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArtistTypeID = Convert.ToInt32(gvArtistTypes.DataKeys[e.Row.RowIndex]["ArtistTypeID"]);

                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                if (hypEdit != null)
                {
                    hypEdit.ToolTip = "Edit";
                    hypEdit.Style["Cursor"] = "hand";
                    hypEdit.Style["border"] = "0";
                    hypEdit.NavigateUrl = "Edit.aspx?ArtistTypeID=" + ArtistTypeID.ToString();
                }

                Button imgDel = (Button)e.Row.FindControl("butDelete");
                if (imgDel != null)
                {
                    imgDel.Attributes.Add("onclick", "return ConfirmDelete()");
                    imgDel.Style["Cursor"] = "hand";
                }
            }
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            txtSearchArtistType.Text = "";
            DoSearch();
        }
    }
}