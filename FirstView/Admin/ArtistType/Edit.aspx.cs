using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.ArtistType
{
    public partial class Edit : System.Web.UI.Page
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
            int ArtistTypeID = 0;
            cArtistType at = new cArtistType();
            DataView dv = new DataView();

            if (Request.QueryString["ArtistTypeID"] != null)
            {
                ArtistTypeID = Convert.ToInt32(Request.QueryString["ArtistTypeID"]);
            }

            dv = at.ListByID(ArtistTypeID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Description"] != DBNull.Value)
                    { txtArtistType.Text = dv.Table.Rows[i]["Description"].ToString(); }
                    else { txtArtistType.Text = ""; }
                }
            }
        }


        protected void butSave_Click(object sender, EventArgs e)
        {
            int ArtistTypeID = 0;
            bool Exists = false;
            cArtistType at = new cArtistType();

            if (Request.QueryString["ArtistTypeID"] != null)
            {
                ArtistTypeID = Convert.ToInt32(Request.QueryString["ArtistTypeID"]);
            }

            Exists = at.CheckExistsEdit(ArtistTypeID, txtArtistType.Text);
            if (Exists == false)
            {
                at.Edit(ArtistTypeID, txtArtistType.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Response.Redirect("List.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalExists();", true);
            }
        }

    }
}