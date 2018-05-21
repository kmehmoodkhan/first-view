using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.Admin.Users
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
            LoadGridView();
        }
        protected void radArtistAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        protected void radArtistActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        protected void radArtistDeleted_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            LoadGridView();
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            radArtistAll.Checked = false;
            radArtistDeleted.Checked = false;
            radArtistActive.Checked = true;
            txtSearchName.Text = "";
            LoadGridView();
        }
        private void LoadGridView()
        {
            cUsers usr = new cUsers();
            DataView dv = new DataView();
            bool? IsDeleted = null;
            string Name = "";

            if (txtSearchName.Text.Length > 0)
            {
                Name = txtSearchName.Text;
            }
           
            if (radArtistAll.Checked == true)
            {
                IsDeleted = null;
            }
            else if (radArtistActive.Checked == true)
            {
                IsDeleted = true;
            }
            else if (radArtistDeleted.Checked == true)
            {
                IsDeleted = false;
            }
            dv = usr.List(IsDeleted,Name);
            if (dv.Table.Rows.Count > 0)
            {
                gvUsers.DataSource = dv.Table;
                gvUsers.DataBind();
                gvUsers.Visible = true;
            }
            else
            { gvUsers.Visible = false; }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            LoadGridView();
        }
        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            cUsers aw = new cUsers();

            if (e.CommandName.Equals("deleteRecord"))
            {
                if (gvUsers.PageIndex > 0)
                { index = Convert.ToInt32(e.CommandArgument) - (gvUsers.PageIndex * gvUsers.PageSize); }
                else { index = Convert.ToInt32(e.CommandArgument); }

                int UserId = Convert.ToInt32(gvUsers.DataKeys[index]["UserID"]);
                //int ArtistPieceID = Convert.ToInt32(gvUsers.DataKeys[index].Values["ArtistPieceID"]);

                string message=aw.Delete(UserId, Session["FV_Username"].ToString());
                LoadGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup('"+ message + "');", true);
            }
        }
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int UserID = 0;
            string IsAdmin = "";
            string IsActive = "";
            string IsArtist = "";
            string IsVerified = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserID = Convert.ToInt32(gvUsers.DataKeys[e.Row.RowIndex]["UserID"]);
                //if (gvUsers.DataKeys[e.Row.RowIndex]["Name"] != DBNull.Value)
                //{ Name = gvUsers.DataKeys[e.Row.RowIndex]["Name"].ToString(); }
                //if (gvUsers.DataKeys[e.Row.RowIndex]["Surname"] != DBNull.Value)
                //{ Surname = gvUsers.DataKeys[e.Row.RowIndex]["Surname"].ToString(); }
                //if (gvUsers.DataKeys[e.Row.RowIndex]["Email"] != DBNull.Value)
                //{ EmailAddress = gvUsers.DataKeys[e.Row.RowIndex]["Email"].ToString(); }
                //if (gvUsers.DataKeys[e.Row.RowIndex]["Username"] != DBNull.Value)
                //{ Username = gvUsers.DataKeys[e.Row.RowIndex]["Username"].ToString(); }
                Button imgDel = (Button)e.Row.FindControl("butDelete");
                if (imgDel != null)
                {
                    
                    imgDel.Style["Cursor"] = "hand";
                }
                if (gvUsers.DataKeys[e.Row.RowIndex]["IsActive"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsActive"]) == true)
                    {
                        IsActive = "Yes";
                        imgDel.Text = "Delete";
                        imgDel.Attributes.Add("onclick", "return ConfirmDelete('Are you sure you want to delete this user?')");
                    }
                    else if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsActive"]) == false)
                    {
                        IsActive = "No";
                        imgDel.Text = "Undelete";
                        imgDel.Attributes.Add("onclick", "return ConfirmDelete('Are you sure you want to undelete this user?')");
                    }
                }
                if (gvUsers.DataKeys[e.Row.RowIndex]["IsAdmin"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsAdmin"]) == true)
                    { IsAdmin = "Yes"; }
                    else if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsAdmin"]) == false)
                    { IsAdmin = "No"; }
                }
                if (gvUsers.DataKeys[e.Row.RowIndex]["IsArtist"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsArtist"]) == true)
                    { IsArtist = "Yes"; }
                    else if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsArtist"]) == false)
                    { IsArtist = "No"; }
                }
                if (gvUsers.DataKeys[e.Row.RowIndex]["IsVerified"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsVerified"]) == true)
                    { IsVerified = "Yes"; }
                    else if (Convert.ToBoolean(gvUsers.DataKeys[e.Row.RowIndex]["IsVerified"]) == false)
                    { IsVerified = "No"; }
                }

                Label lblIsActive = (Label)e.Row.FindControl("lblIsActive");
                if (lblIsActive != null)
                {
                    lblIsActive.Text = IsActive;
                    if (IsActive == "Yes")
                    { lblIsActive.CssClass = "text-success"; }
                    else if (IsActive == "No")
                    { lblIsActive.CssClass = "text-danger"; }
                }
                Label lblIsAdmin = (Label)e.Row.FindControl("lblIsAdmin");
                if (lblIsAdmin != null)
                {
                    lblIsAdmin.Text = IsAdmin;
                    if (IsAdmin == "Yes")
                    { lblIsAdmin.CssClass = "text-success"; }
                    else if (IsAdmin == "No")
                    { lblIsAdmin.CssClass = "text-danger"; }
                }
                Label lblIsArtist = (Label)e.Row.FindControl("lblIsArtist");
                if (lblIsArtist != null)
                {
                    lblIsArtist.Text = IsArtist;
                    if (IsArtist == "Yes")
                    { lblIsArtist.CssClass = "text-success"; }
                    else if (IsArtist == "No")
                    { lblIsArtist.CssClass = "text-danger"; }
                }
                Label lblIsVerified = (Label)e.Row.FindControl("lblIsVerified");
                if (lblIsVerified != null)
                {
                    lblIsVerified.Text = IsVerified;
                    if (IsVerified == "Yes")
                    { lblIsVerified.CssClass = "text-success"; }
                    else if (IsVerified == "No")
                    { lblIsVerified.CssClass = "text-danger"; }
                }
                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                if (hypEdit != null)
                {
                    hypEdit.ToolTip = "Edit";
                    hypEdit.Style["Cursor"] = "hand";
                    hypEdit.Style["border"] = "0";
                    hypEdit.NavigateUrl = "Edit.aspx?UserID=" + UserID.ToString();
                }

               

                HyperLink butResetPass = (HyperLink)e.Row.FindControl("butResetPass");
                if (butResetPass != null)
                {
                    butResetPass.NavigateUrl = "ResetPassword.aspx?UserID=" + UserID.ToString();
                }
            }
        }
    }
}