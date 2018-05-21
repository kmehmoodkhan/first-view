using System;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.ArtistType
{
    public partial class Add : System.Web.UI.Page
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
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            bool Exists = false;
            cArtistType at = new cArtistType();

            Exists = at.CheckExistsAdd(txtArtistType.Text);
            if (Exists == false)
            {
                at.Add(txtArtistType.Text);
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