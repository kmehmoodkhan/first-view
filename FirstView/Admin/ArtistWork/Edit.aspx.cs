using System;
using System.Data;
using System.Web.UI;
using FirstView.BusinessLayer;

namespace FirstView.Admin.ArtistWork
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                LoadData();
                CalculateCommission();
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
            BindPresenationTypes();
            BindWorkEditionTypes();
            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();

            int ArtistWorkID = 0;
            string ImageFileName = "";
            int ArtistID = 0;
            string Name = "";
            string Surname = "";
            BindCurrentEventDetail();
            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }
            if (Request.QueryString["ArtistWorkID"] != null)
            {
                ArtistWorkID = Convert.ToInt32(Request.QueryString["ArtistWorkID"]);
            }


           

            string commission = aw.GetArtistCommission(ArtistID);
            ViewState["Commission"] = commission;

            if (string.IsNullOrEmpty(commission) || commission.Trim() == "0")
            {
                divApproximateWallPrice.Visible = true;
            }
            else
            {
                divApproximateWallPrice.Visible = false;
            }

            btnBack.HRef = "List.aspx?ArtistID=" + ArtistID.ToString();

            

            dv = aw.ListByID(ArtistWorkID);
            if (dv.Table.Rows.Count > 0)
            {
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
                    {
                        txtPrice.Text = Math.Round(Convert.ToDecimal(dv.Table.Rows[i]["WallPrice"])).ToString();
                    }


                    if (dv.Table.Rows[i]["ArtistPrice"] != DBNull.Value)
                    {
                        txtArtistPrice.Text = Math.Round(Convert.ToDecimal(dv.Table.Rows[i]["ArtistPrice"])).ToString();
                    }


                    if (dv.Table.Rows[i]["Width"] != DBNull.Value)
                    { txtWidth.Text = dv.Table.Rows[i]["Width"].ToString(); }
                    if (dv.Table.Rows[i]["Height"] != DBNull.Value)
                    { txtHeight.Text = dv.Table.Rows[i]["Height"].ToString(); }
                    if (dv.Table.Rows[i]["ImageFileName"] != DBNull.Value)
                    { ImageFileName = dv.Table.Rows[i]["ImageFileName"].ToString(); }
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Note"] != DBNull.Value)
                    { txtNote.Text = dv.Table.Rows[i]["Note"].ToString(); }

                    if (dv.Table.Rows[i]["Exhibition"] != DBNull.Value)
                    { lblExhibitionName.Text = dv.Table.Rows[i]["Exhibition"].ToString(); }
                    if (dv.Table.Rows[i]["ExhibitionNo"] != DBNull.Value)
                    {
                        hdnfExhibitionNo.Value = dv.Table.Rows[i]["ExhibitionNo"].ToString();
                        if (hdnfExhibitionNo.Value != "0" && !string.IsNullOrEmpty(hdnfExhibitionNo.Value))
                        {
                            chkIsParticipating.Checked = true;
                        }
                    }

                    if (dv.Table.Rows[i]["ApproximatePrice"] != DBNull.Value)
                    {
                        this.txtApproximateWallPrice.Text = dv.Table.Rows[i]["ApproximatePrice"].ToString();
                    }

                    
                }

                lblArtistName.Text = Name + " " + Surname;
                imgArtistWork.ImageUrl = "../../Uploads/Thumbnails/" + ImageFileName;
            }

            //if (!string.IsNullOrEmpty(commission) && commission != "0")
            //{
            //    divWallPrice.Visible = true;
            //    divArtistPrice.Visible = false;
            //}
            //else
            //{
            //    divWallPrice.Visible = false;
            //    divArtistPrice.Visible = true;
            //}


            

            if (lblExhibitionName.Text == "-N/A-")
            {
                chkIsParticipating.Visible = false;
            }
            else
            {
                chkIsParticipating.Visible = true;

            }
            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;

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
            int ArtistWorkID = 0;
            int ArtistID = 0;

            string Price = "";
            string ArtistPrice = "";

            string estimatedPrice = "";
            int Width = 0;
            int Height = 0;
            int presentationTypeId = 0;
            int workEditionTypeId = 0;
            int editionNumber = 0;
            int highestEditionNumber = 0;
            cArtistWorks aw = new cArtistWorks();

            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }

            string commission = aw.GetArtistCommission(ArtistID);


            if (Request.QueryString["ArtistWorkID"] != null)
            {
                ArtistWorkID = Convert.ToInt32(Request.QueryString["ArtistWorkID"]);
            }
            if (txtPrice.Text.Length > 0)
            {
                Price = txtPrice.Text;
            }

            if (txtArtistPrice.Text.Length > 0)
            {
                ArtistPrice = txtArtistPrice.Text;
            }

            if (!string.IsNullOrEmpty(txtPrice.Text) && (commission.Trim() != "0") && !string.IsNullOrEmpty(commission))
            {
                ArtistPrice = ((Convert.ToDecimal(Price) * (100 - Convert.ToDecimal(commission))) / 100).ToString();
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

            aw.Edit(ArtistWorkID, txtWorkName.Text, txtMedium.Text.Trim(), Price,ArtistPrice, Width, Height, hidUniqueID.Value, txtNote.Text, Session["FV_Username"].ToString(), exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber, estimatedPrice,true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            Response.Redirect("List.aspx?ArtistID=" + ArtistID.ToString());
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString(), true);
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
                    this.txtApproximateWallPrice.Text = (price / 0.6).ToString();

                CalculateCommission();
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
                    this.txtApproximateWallPrice.Text = (price / 0.6).ToString();

                CalculateCommission();
            }
        }

        private void CalculateCommission()
        {
            int ArtistID = 0;
            if (Request.QueryString["ArtistID"] != null)
            {
                ArtistID = Convert.ToInt32(Request.QueryString["ArtistID"]);
            }

            cArtistWorks aw = new cArtistWorks();
            string commission = aw.GetArtistCommission(ArtistID);

            if ((commission.Trim() == "0") || string.IsNullOrEmpty(commission))
            {
                decimal artistPrice = 0;
                decimal wallPrice = 0;

                if (!string.IsNullOrEmpty(this.txtArtistPrice.Text))
                {
                    artistPrice = decimal.Parse(this.txtArtistPrice.Text);
                }

                if (!string.IsNullOrEmpty(this.txtPrice.Text))
                {
                    wallPrice = decimal.Parse(this.txtPrice.Text);
                }


                decimal commissiom = 0;

                commissiom = ((wallPrice - artistPrice) / wallPrice) * 100;

                this.txtCommission.Text = Math.Round(commissiom).ToString();
            }
        }
    }
}