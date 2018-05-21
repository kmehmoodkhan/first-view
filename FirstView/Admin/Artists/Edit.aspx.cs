using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.Admin.Artists
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
            int ArtistID = 0;
            cArtist a = new cArtist();
            cApprovals appr = new cApprovals();
            DataView dv = new DataView();

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }

            cArtistType at = new cArtistType();
            DataView dv2 = new DataView();

            dv2 = at.List();
            if (dv2.Table.Rows.Count > 0)
            {
                ListItem li1 = new ListItem();
                li1.Text = "--Please Select--";
                li1.Value = "";
                ddlArtistType.Items.Add(li1);
                for (int i = 0; i < dv2.Table.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = dv2.Table.Rows[i]["Description"].ToString();
                    li.Value = dv2.Table.Rows[i]["ArtistTypeID"].ToString();
                    ddlArtistType.Items.Add(li);
                }
            }

            dv = a.ListByID(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { txtName.Text = dv.Table.Rows[i]["Name"].ToString(); }
                    else { txtName.Text = ""; }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { txtSurname.Text = dv.Table.Rows[i]["Surname"].ToString(); }
                    else { txtSurname.Text = ""; }
                    if (dv.Table.Rows[i]["CV"] != DBNull.Value)
                    { txtCV.Text = dv.Table.Rows[i]["CV"].ToString(); }
                    else { txtCV.Text = ""; }
                    if (dv.Table.Rows[i]["ImageFileName"] != DBNull.Value)
                    {
                        imgArtist.ImageUrl = "../../Uploads/Thumbnails/" + dv.Table.Rows[i]["ImageFileName"].ToString();
                        imgArtist.Visible = true;
                    }
                    else { imgArtist.Visible = false; }
                    if (dv.Table.Rows[i]["ArtistTypeID"] != DBNull.Value)
                    {
                        ddlArtistType.SelectedValue = dv.Table.Rows[i]["ArtistTypeID"].ToString();
                    }
                    if (dv.Table.Rows[i]["IsDeleted"] != DBNull.Value)
                    {
                        chkIsActive.Checked =Convert.ToBoolean(dv.Table.Rows[i]["IsDeleted"])==true?false:true;
                    }
                    else
                    {
                        chkIsActive.Checked = true;
                    }
                }
            }

            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;
        }


        protected void butSave_Click(object sender, EventArgs e)
        {
            int ArtistID = 0;

            cArtist a = new cArtist();

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }

            a.Edit(ArtistID, txtName.Text, txtSurname.Text, txtCV.Text, Convert.ToInt32(ddlArtistType.SelectedValue), chkIsActive.Checked, hidUniqueID.Value, Session["FV_Username"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            Response.Redirect("List.aspx");
        }

    }
}