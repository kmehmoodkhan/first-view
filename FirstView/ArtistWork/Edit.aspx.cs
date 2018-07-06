using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.ArtistWork
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CheckUserValidity();
                if (!Page.IsPostBack)
                {
                    LoadData();
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
        private void LoadData()
        {
            BindCurrentEventDetail();
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();

            int ArtistWorkID = 0;
            string ImageFileName = "";
            int ArtistID = 0;

            ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);

            if (Request.QueryString["ArtistWorkID"] != null)
            {
                ArtistWorkID = Convert.ToInt32(Request.QueryString["ArtistWorkID"]);
            }



            dv = aw.ListByID(ArtistWorkID);
            if (dv.Table.Rows.Count > 0)
            {
                BindPresenationTypes();
                BindWorkEditionTypes();
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["WorkName"] != DBNull.Value)
                    { txtWorkName.Text = dv.Table.Rows[i]["WorkName"].ToString(); }
                    if (dv.Table.Rows[i]["Medium"] != DBNull.Value)
                    { txtMedium.Text = dv.Table.Rows[i]["Medium"].ToString(); }
                    if (dv.Table.Rows[i]["PresentationTypeId"] != DBNull.Value)
                    { rbltPresenation.SelectedValue = dv.Table.Rows[i]["PresentationTypeId"].ToString(); }
                    if (dv.Table.Rows[i]["WorkEditionTypeId"] != DBNull.Value)
                    {
                        rbltEditionType.SelectedValue = dv.Table.Rows[i]["WorkEditionTypeId"].ToString();
                        if (rbltEditionType.SelectedValue == "2")
                        {
                            rowEdition.Visible = true;
                            rowNum.Visible = true;
                            txtEditionNumber.Text = Convert.ToString(dv.Table.Rows[i]["EditionNumber"]);
                            txtHeighestEdition.Text = Convert.ToString(dv.Table.Rows[i]["HighestEdition"]);
                        }
                    }


                    if (dv.Table.Rows[i]["WallPrice"] != DBNull.Value)
                    { txtPrice.Text = Math.Round(Convert.ToDecimal(dv.Table.Rows[i]["WallPrice"]), 0).ToString(); }

                    if (dv.Table.Rows[i]["ArtistPrice"] != DBNull.Value)
                    { txtArtistPrice.Text = Math.Round(Convert.ToDecimal(dv.Table.Rows[i]["ArtistPrice"]), 0).ToString(); }

                    if (dv.Table.Rows[i]["Width"] != DBNull.Value)
                    { txtWidth.Text = Convert.ToInt32(dv.Table.Rows[i]["Width"]).ToString(); }
                    if (dv.Table.Rows[i]["Height"] != DBNull.Value)
                    { txtHeight.Text = Convert.ToInt32(dv.Table.Rows[i]["Height"]).ToString(); }
                    if (dv.Table.Rows[i]["Note"] != DBNull.Value)
                    { txtNote.Text = dv.Table.Rows[i]["Note"].ToString(); }
                    if (dv.Table.Rows[i]["ImageFileName"] != DBNull.Value)
                    { ImageFileName = dv.Table.Rows[i]["ImageFileName"].ToString(); }
                    if (dv.Table.Rows[i]["Exhibition"] != DBNull.Value)
                    { lblExhibitionName.Text = dv.Table.Rows[i]["Exhibition"].ToString(); }
                    hdnfExhibitionNo.Value = dv.Table.Rows[i]["CurrentExhibitionNo"].ToString();
                    if (dv.Table.Rows[i]["ExhibitionNo"] != DBNull.Value && dv.Table.Rows[i]["CurrentExhibitionNo"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(dv.Table.Rows[i]["ExhibitionNo"]) == Convert.ToInt32(dv.Table.Rows[i]["CurrentExhibitionNo"]))
                        {
                            chkIsParticipating.Checked = true;
                        }
                    }
                }
                imgArtistWork.ImageUrl = "../Uploads/Thumbnails/" + ImageFileName;
                DataTable dt = null;
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

            string commission = aw.GetArtistCommission(ArtistID);

            if (!string.IsNullOrEmpty(commission) && commission != "0")
            {
                divWallPrice.Visible = true;
                divArtistPrice.Visible = false;
                divApproximateWallPrice.Visible = false;
                if (!string.IsNullOrEmpty(this.txtPrice.Text))
                {
                    this.txtApproximateWallPrice.Text = (Math.Round(Convert.ToDouble(this.txtPrice.Text) * 1.0 / 0.6, 2)).ToString();
                }

            }
            else
            {
                divWallPrice.Visible = false;
                divArtistPrice.Visible = true;
                divApproximateWallPrice.Visible = true;
                if (!string.IsNullOrEmpty(this.txtArtistPrice.Text))
                {
                    this.txtApproximateWallPrice.Text = (Math.Round(Convert.ToDouble(this.txtArtistPrice.Text) * 1.0 / 0.6, 2)).ToString();
                }
            }


            if (lblExhibitionName.Text == "-N/A-")
            {
                chkIsParticipating.Visible = false;
            }
            else
            {
                chkIsParticipating.Visible = true;
            }
            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;

        }
        private void BindCurrentEventDetail()
        {
            cEvents objcEvents = new cEvents();
            DataView dv = objcEvents.CurrentExhibition();
            DataTable dt = dv.ToTable();
            hdnfExhibitionNo.Value = Convert.ToString(dt.Rows[0]["ExhibitionNo"]);
            lblExhibitionName.Text = Convert.ToString(dt.Rows[0]["CurrentExhibition"]);
        }
        protected void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ArtistWorkID = 0;
                int ArtistID = 0;
                string artistPrice = "";
                string wallPrice = "";
                string estimatedPrice = "";
                int Width = 0;
                int Height = 0;
                int presentationTypeId = 0;
                int workEditionTypeId = 0;
                int editionNumber = 0;
                int highestEditionNumber = 0;
                cArtistWorks aw = new cArtistWorks();



                ArtistID = Convert.ToInt32(Session["FV_ArtistID"]);

                string commission = aw.GetArtistCommission(ArtistID);
                if (Request.QueryString["ArtistWorkID"] != null)
                {
                    ArtistWorkID = Convert.ToInt32(Request.QueryString["ArtistWorkID"]);
                }
                if (txtPrice.Text.Length > 0)
                {
                    wallPrice = txtPrice.Text;
                }

                if (txtArtistPrice.Text.Length > 0)
                {
                    artistPrice = txtArtistPrice.Text;
                }

                if (!string.IsNullOrEmpty(txtPrice.Text) && (commission.Trim() != "0") && !string.IsNullOrEmpty(commission))
                {
                    artistPrice = ((Convert.ToDecimal(wallPrice) * (100 - Convert.ToDecimal(commission))) / 100).ToString();
                }

                if (!string.IsNullOrEmpty(this.txtApproximateWallPrice.Text))
                {
                    estimatedPrice = this.txtApproximateWallPrice.Text;
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

                aw.Edit(ArtistWorkID, txtWorkName.Text, txtMedium.Text.Trim(), wallPrice, artistPrice, Width, Height, hidUniqueID.Value, txtNote.Text, Session["FV_Username"].ToString(), exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber, estimatedPrice, false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Response.Redirect("Main.aspx");

            }
            catch (Exception)
            {
                throw;
            }
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
        protected void lnk_ServerClick(object sender, EventArgs e)
        {
            string imgSrc = imgArtistWork.ImageUrl;
            string filename = System.IO.Path.GetFileName(MapPath(imgSrc));
            string filePath = MapPath(imgSrc);
            filePath = filePath.Replace("Uploads\\Thumbnails", "Uploads\\Original");
            Response.ContentType = "image/JPEG";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {

            if (txtPrice.Text.Trim().Length > 0)
            {
                int price = 0;
                bool isValidNo = true;
                try
                {
                    price = Convert.ToInt32(txtPrice.Text);
                }
                catch (Exception ex)
                {
                    isValidNo = false;
                }
                if (isValidNo)
                    this.txtApproximateWallPrice.Text = (Math.Round(price * 1.0 / 0.6, 2)).ToString();
            }
        }

        protected void txtArtistPrice_TextChanged(object sender, EventArgs e)
        {
            if (txtArtistPrice.Text.Trim().Length > 0)
            {
                int price = 0;
                bool isValidNo = true;
                try
                {
                    price = Convert.ToInt32(txtArtistPrice.Text);
                }
                catch (Exception ex)
                {
                    isValidNo = false;
                }
                if (isValidNo)
                    this.txtApproximateWallPrice.Text = (Math.Round(price * 1.0 / 0.6, 2)).ToString();
            }
        }
    }
}