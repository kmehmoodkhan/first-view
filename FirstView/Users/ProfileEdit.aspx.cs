using System;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Common;
namespace FirstView.PublicView
{
    public partial class ProfileEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void CheckUserValidity()
        {
            //Reset session if expired
            if (Session["ArtistID"] == null)
            {
                Response.Redirect("../Users/Login.aspx?im=3");
            }
        }

        private void LoadData()
        {
            int ArtistID = 0;
            cArtist a = new cArtist();
            DataView dv = new DataView();
            cApprovals appr = new cApprovals();

            ArtistID = Convert.ToInt32(Session["ArtistID"]);

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
                        imgArtist.ImageUrl = "../Uploads/Thumbnails/" + dv.Table.Rows[i]["ImageFileName"].ToString();
                        imgArtist.Visible = true;
                    }
                    else { imgArtist.Visible = false; }
                    if (dv.Table.Rows[i]["ArtistTypeID"] != DBNull.Value)
                    {
                        ddlArtistType.SelectedValue = dv.Table.Rows[i]["ArtistTypeID"].ToString();
                    }
                }
            }

            hidUniqueID.Value = Guid.NewGuid().ToString();
            ifrmUpload.Src = "../Utils/Upload.aspx?UniqueID=" + hidUniqueID.Value;

            if (Request.QueryString["Reg"] != null)
            {
                if (Request.QueryString["Reg"].ToString() == "1")
                {
                    registration.Style["Display"] = "";
                }
            }
        }

        protected void butSave_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void DoSave()
        {
            int artistId = Convert.ToInt32(Session["ArtistID"]);
            cArtist a = new cArtist();
            DataTable dvUserDetails = a.UserDetailsByArtistId(artistId).ToTable();
            a.Edit(artistId, txtName.Text, txtSurname.Text, txtCV.Text, Convert.ToInt32(ddlArtistType.SelectedValue), true, hidUniqueID.Value, Convert.ToString(dvUserDetails.Rows[0]["Username"]));
            cUsers cUsers = new cUsers();
            DataView dv = cUsers.ListAllAdmins();
            ArrayList alEmailIds = new ArrayList();
            string emailId = string.Empty;
            int adminId = 0;
            foreach (DataRow dr in dv.Table.Rows)
            {
                emailId = Convert.ToString(dr["Email"]);
                adminId = Convert.ToInt32(dr["UserId"]);
                if (!alEmailIds.Contains(emailId))
                {
                    alEmailIds.Add(emailId);
                }
            }
            if (alEmailIds != null && alEmailIds.Count > 0)
            {
                for (int i = 0; i < alEmailIds.Count; i++)
                {
                    SendEmail(artistId, 1, alEmailIds[i].ToString(), adminId);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            //Response.Redirect("Menu.aspx");
        }



        private void SendEmail(int ArtistID, int ApprovalStatus, string EmailAddress, int adminId)
        {
            string Surname = "";
            string Name = "";
            string FromEmailAddress = "";
            string smtpClientHost = "";
            string smtpClientPort = "";
            string Subject = "";
            StringBuilder sbBody = new StringBuilder();
            CommonLibrary cl = new CommonLibrary();
            cArtist aw = new cArtist();
            cSettings set = new cSettings();
            DataView dv = new DataView();

            dv = aw.ListByIDForEmail(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }

                }
            }

            Subject = "First-View Gallery Notification - Artist Page - Submitted For Approval";

            SmtpClient smtpClient = new SmtpClient();

            FromEmailAddress = set.ListByCode("SystemEmailAddress");
            smtpClientHost = set.ListByCode("smtpClientHost");
            smtpClientPort = set.ListByCode("smtpClientPort");

            MailMessage message = new MailMessage();
            MailAddress mailFrom = new MailAddress(FromEmailAddress);

            message.To.Add(EmailAddress);
            message.IsBodyHtml = true;
            message.From = mailFrom;

            sbBody.AppendLine("<b>Artist Page - Submitted For Approval</b><br/><br/>");
            sbBody.AppendLine("Good day, <br/>");
            sbBody.AppendLine(String.Concat("The Artist: ", Name, " ", Surname, " has submitted their page for approval. <br/>"));

            string url = "http://first-view.uk/users/Login.aspx?papr=1&ArtistId=" + ArtistID + "&AdId=" + adminId;

            sbBody.AppendLine(String.Concat("<a href='" + url + "'> Click here to approve the page.</a><br/><br/>"));

            sbBody.AppendLine("Thank You<br/>");
            sbBody.AppendLine("Admin @ First-View");

            message.Subject = Subject;
            message.Body = sbBody.ToString();

            smtpClient.Host = smtpClientHost;
            smtpClient.Port = Convert.ToInt32(smtpClientPort);

            smtpClient.Send(message);
            message.Dispose();
        }

        protected void butPreview_Click(object sender, EventArgs e)
        {
            DoSave();
            Response.Redirect("PreviewArtistProfile.aspx?RetUrl=1");
        }
    }
}