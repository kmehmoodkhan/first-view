using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
namespace FirstView.Artist
{
    public partial class ProfileAdd : System.Web.UI.Page
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
                Response.Redirect("../Users/Login.aspx?im=3");
            }
            if (Session["FV_UserID"].ToString().Length == 0)
            {
                Response.Redirect("../Users/Login.aspx?im=3");
            }
        }

        private void LoadData()
        {
            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "Upload.aspx?UniqueID=" + hidUniqueID.Value;

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
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            bool ImageExists = false;
            int ArtistID = 0;

            cTempImages ti = new cTempImages();
            cArtist a = new cArtist();

            ImageExists = ti.CheckImageExists(hidUniqueID.Value);
            if (ImageExists == true)
            {
                ArtistID = a.Add(txtName.Text, txtSurname.Text, txtCV.Text, Convert.ToInt32(ddlArtistType.SelectedValue), hidUniqueID.Value, Session["FV_Username"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Response.Redirect("ProfileEdit.aspx?ArtistID=" + ArtistID.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalNoImage();", true);
            }
        }        

        protected void butBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}