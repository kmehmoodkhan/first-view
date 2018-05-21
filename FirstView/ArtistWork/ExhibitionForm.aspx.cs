using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstView.BusinessLayer;

namespace FirstView.ArtistWork
{
    public partial class ExhibitionForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
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

        private void LoadGridView()
        {
            int ArtistID = 0;
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();
            string artistId = Convert.ToString(Request.QueryString["artistid"]);
            if (!string.IsNullOrEmpty(artistId))
            {
                ArtistID = Convert.ToInt32(artistId);
            }
            else
            {
                ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);
            }
            dv = aw.ListByArtistID(ArtistID, 0);
            if (dv.Table.Rows.Count > 0)
            {
                gvArtistWorks.DataSource = dv.Table;
                gvArtistWorks.DataBind();
                gvArtistWorks.Visible = true;

            }
            else
            {
                gvArtistWorks.Visible = false;

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", "showAddModal();", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add.aspx");
        }
        protected void gvArtistWorks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal Commission = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(gvArtistWorks.DataKeys[e.Row.RowIndex]["Commission"])))
                {
                    Commission = Convert.ToInt32(gvArtistWorks.DataKeys[e.Row.RowIndex]["Commission"]);
                }
                if(Commission>0)
                {
                    TextBox txtArtistPrice = e.Row.FindControl("txtArtistPrice") as TextBox;
                    txtArtistPrice.Visible = false;
                }
                else
                {
                    TextBox txtWallPrice = e.Row.FindControl("txtWallPrice") as TextBox;
                    txtWallPrice.Visible = false;
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}