using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.ArtistWork
{
    public partial class Add : System.Web.UI.Page
    {
        public static string FileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                BindPresenationTypes();
                BindWorkEditionTypes();
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
            string Surname = "";
            string Name = "";

            cArtist a = new cArtist();
            DataView dv = new DataView();

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }



            dv = a.ListByID(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                }
                lblArtistName.Text = Name + " " + Surname;
            }

            btnBack.HRef = "List.aspx?ArtistID=" + ArtistID.ToString();

            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;
            BindCurrentEventDetail();
        }
        private void BindCurrentEventDetail()
        {
            cEvents objcEvents = new cEvents();
            DataView dv = objcEvents.CurrentExhibition();
            DataTable dt = dv.ToTable();
            hdnfExhibitionNo.Value = Convert.ToString(dt.Rows[0]["ExhibitionNo"]);
            lblExhibitionName.Text = Convert.ToString(dt.Rows[0]["CurrentExhibition"]);
            if (lblExhibitionName.Text == "-N/A-")
            {
                chkIsParticipating.Visible = false;
            }
            else
            {
                chkIsParticipating.Visible = true;

            }
        }
        private void BindPresenationTypes()
        {
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();
            dv = aw.PresetntationTypes();
            rbltPresenation.DataSource = dv.ToTable();
            rbltPresenation.DataTextField = "PresentationType";
            rbltPresenation.DataValueField = "PresentationTypeID";
            rbltPresenation.DataBind();
        }
        private void BindWorkEditionTypes()
        {
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();
            dv = aw.WorkEditionTypes();
            rbltEditionType.DataSource = dv.ToTable();
            rbltEditionType.DataTextField = "WorkEditionType";
            rbltEditionType.DataValueField = "WorkEditionTypeId";
            rbltEditionType.DataBind();
        }
        protected void butSave_Click(object sender, EventArgs e)
        {
            int ArtistPieceID = 0;
            int ArtistID = 0;
            string WallPrice = "";
            string ArtistPrice = "";
            int Width = 0;
            int Height = 0;
            int presentationTypeId = 0;
            int workEditionTypeId = 0;
            int editionNumber = 0;
            int highestEditionNumber = 0;
            bool ImageExists = false;

            cTempImages ti = new cTempImages();
            cArtistWorks aw = new cArtistWorks();

            ImageExists = ti.CheckImageExists(hidUniqueID.Value);
            if (ImageExists == true)
            {
                if (Request.QueryString["ArtistID"] != null)
                {
                    ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
                }
                if (txtPrice.Text.Length > 0)
                {
                    WallPrice = txtPrice.Text;
                }

                if (!string.IsNullOrEmpty(txtArtistPrice.Text))
                {
                    ArtistPrice = this.txtArtistPrice.Text;
                }

                string commission = aw.GetArtistCommission(ArtistID);
                if (!string.IsNullOrEmpty(txtPrice.Text) && (commission.Trim() != "0") && !string.IsNullOrEmpty(commission))
                {
                    ArtistPrice = ((Convert.ToDecimal(WallPrice) * (100 - Convert.ToDecimal(commission))) / 100).ToString();
                }


                if (txtWidth.Text.Length > 0)
                {
                    Width = Convert.ToInt32(txtWidth.Text);
                }
                if (txtHeight.Text.Length > 0)
                {
                    Height = Convert.ToInt32(txtHeight.Text);
                }
                int exhibitionNo = 0;
                if (chkIsParticipating.Checked)
                {
                    exhibitionNo = Convert.ToInt32(hdnfExhibitionNo.Value.Trim());
                }


                presentationTypeId = Convert.ToInt32(rbltPresenation.SelectedValue);

                workEditionTypeId = Convert.ToInt32(rbltEditionType.SelectedValue);
                if (rbltEditionType.SelectedValue == "2")
                {
                    if (!string.IsNullOrEmpty(txtEditionNumber.Text.Trim()) && !string.IsNullOrEmpty(txtHeighestEdition.Text.Trim()))
                    {
                        editionNumber = Convert.ToInt32(txtEditionNumber.Text.Trim());
                        highestEditionNumber = Convert.ToInt32(txtHeighestEdition.Text.Trim());
                    }
                    else
                    {
                        lblErrorMessage.Text = "For Limited Edition Work, number and edition fields are required. There should be valid integer value for number and edition fields.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowError();", true);
                        return;
                    }
                }

                ArtistPieceID = aw.Add(txtWorkName.Text, txtMedium.Text.Trim(), WallPrice,ArtistPrice, Width, Height, ArtistID, hidUniqueID.Value, txtNote.Text, Session["FV_Username"].ToString(), exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber,string.Empty);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                Response.Redirect("List.aspx?ArtistID=" + ArtistID.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalNoImage();", true);
            }
        }
        protected void rbltEditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbltEditionType.SelectedValue == "2")
            {
                rowEdition.Visible = true;
                rowNum.Visible = true;
            }
            else
            {
                rowEdition.Visible = false;
                rowNum.Visible = false;

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString(), true);
        }
    }
}