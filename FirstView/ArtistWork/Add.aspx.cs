using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;
namespace FirstView.ArtistWork
{
    public partial class Add : System.Web.UI.Page
    {
        public static string FileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CheckUserValidity();
                if (!Page.IsPostBack)
                {
                    LoadData();
                    BindPresenationTypes();
                    BindWorkEditionTypes();
                }
            }
            catch (Exception)
            {

                throw;
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
            int ArtistID = 0;
            ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);
            btnBack.HRef = "Main.aspx";

            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;
            BindCurrentEventDetail();


        }
        private void BindCurrentEventDetail()
        {
            DataView dv = null;
            DataTable dt = null;
            try
            {
                cEvents objcEvents = new cEvents();
                dv = objcEvents.CurrentExhibition();
                dt = dv.ToTable();
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
                int ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);
                cArtist cArtist = new cArtist();
                dv = cArtist.IsArtitstAllowedInExhibition(ArtistID);
                if (dv != null)
                {
                    dt = dv.ToTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0][0]) == 1)
                        {
                            addexhitionrow.Visible = true;
                        }
                        else
                        {
                            addexhitionrow.Visible = false;
                            hdnfExhibitionNo.Value = "0";
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dv != null)
                {
                    dv.Dispose();

                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                cTempImages ti = new cTempImages();
                cArtistWorks aw = new cArtistWorks();

                int ArtistWorkID = 0;
                int ArtistID = 0;
                string Price = "";
                int Width = 0;
                int Height = 0;

                ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);
                if (txtPrice.Text.Length > 0)
                {
                    Price = txtPrice.Text;
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
                int presentationTypeId = 0;
                int workEditionTypeId = 0;
                int editionNumber = 0;
                int highestEditionNumber = 0;

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
                ArtistWorkID = aw.Add(txtWorkName.Text, txtMedium.Text.Trim(), Price, Width, Height, ArtistID, hidUniqueID.Value, txtNote.Text, Session["FV_Username"].ToString(), exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber);
                DoReset();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception)
            {
                throw;
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

        private void DoReset()
        {
            BindWorkEditionTypes();
            BindPresenationTypes();
            txtWorkName.Text = "";
            txtPrice.Text = "";
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtNote.Text = "";
            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;
        }

        protected void butBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
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