using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;
namespace FirstView.Artist
{
    public partial class IndexView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            int ArtistID = 0;

            cArtist art = new cArtist();
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();
            DataView dv2 = new DataView();

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }

            dv = art.ListByIDForViewing(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    lblFullname.Text = dv.Table.Rows[i]["Name"].ToString() + " " + dv.Table.Rows[i]["Surname"].ToString();
                    lblCV.Text = dv.Table.Rows[i]["CV"].ToString();
                    imgArtist.ImageUrl = "~/Uploads/Thumbnails/" + dv.Table.Rows[i]["ImageFileName"].ToString();
                }
            }

            dv2 = aw.ListByArtistIDForViewing(ArtistID);
            if (dv2.Table.Rows.Count > 0)
            {
                ArtistWork.DataSource = dv2.Table;
                ArtistWork.DataBind();
            }            
        }

        protected void butMyProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileEdit.aspx");
        }

        protected void butArtistPieces_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ArtistPiece/Main.aspx");
        }

        protected void butLoadImages_Click(object sender, EventArgs e)
        {
            int ArtistID = 1;

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }           
        }             
    }
}