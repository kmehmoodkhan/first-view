using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;

namespace FirstView.Users
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblStatus.Visible = false;
                if (Request.QueryString["LogOut"] != null)
                {
                    if (Request.QueryString["LogOut"].ToString() == "1")
                    {
                        Session["FV_UserID"] = null;
                        Session["FV_ArtistID"] = null;
                        Session["FV_EmailAddress"] = null;
                        Session["FV_Name"] = null;
                        Session["FV_Surname"] = null;
                        Session["FV_IsAdmin"] = null;
                        Session["FV_IsArtist"] = null;
                        Session["FV_Username"] = null;
                        Session.Abandon();
                        lblStatus.Text = "You have been logged off successfully";
                        lblStatus.Visible = true;
                    }
                }
                if (Request.QueryString["im"] != null)
                {
                    if (Request.QueryString["im"].ToString() == "1")
                    {
                        lblStatus.Text = "Your Password has been reset successfully. Please login to continue.";
                        lblStatus.CssClass = "text-success";
                        lblStatus.Visible = true;
                    }
                    if (Request.QueryString["im"].ToString() == "2")
                    {
                        lblStatus.Text = "You have aborted the Password Recovery process. Please login to continue.";
                        lblStatus.CssClass = "text-success";
                        lblStatus.Visible = true;
                    }
                    if (Request.QueryString["im"].ToString() == "3")
                    {
                        lblStatus.Text = "Your session has expired. Please login to continue.";
                        lblStatus.CssClass = "text-danger";
                        lblStatus.Visible = true;
                    }
                }

                if (Request.QueryString["papr"] != null)
                {
                    int newStatus = 2;
                    int artistId = Convert.ToInt32(Request.QueryString["ArtistId"]);
                    int adId = Convert.ToInt32(Request.QueryString["AdId"]);

                    cArtist a = new cArtist();
                    a.UpdateProfileStatus(artistId, newStatus, adId);

                    lblStatus.Text = "Artist profile has been approved successfully. Please login to continue.";
                    lblStatus.CssClass = "text-success";
                    lblStatus.Visible = true;
                }

                txtUsername.Focus();
            }
        }

        protected void butLogin_Click(object sender, EventArgs e)
        {
            bool IsValidLogin = false;
            int UserID = 0;
            int ArtistID = 0;
            string Name = "";
            string Surname = "";
            string EmailAddress = "";
            bool IsAdmin = false;
            bool IsArtist = false;
            string Username = "";
            cUsers usr = new cUsers();
            DataView dv = new DataView();

            Session.RemoveAll();
            Session.Clear();
            IsValidLogin = usr.DoAuthenticate(txtUsername.Text, txtPassword.Text);
            if (IsValidLogin == true)
            {
                dv = usr.ListForLogin(txtUsername.Text, txtPassword.Text);
                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        if (dv.Table.Rows[i]["UserID"] != DBNull.Value)
                        {
                            UserID = Convert.ToInt32(dv.Table.Rows[i]["UserID"]);
                        }
                        if (dv.Table.Rows[i]["ArtistID"] != DBNull.Value)
                        {
                            ArtistID = Convert.ToInt32(dv.Table.Rows[i]["ArtistID"]);
                        }
                        if (dv.Table.Rows[i]["Email"] != DBNull.Value)
                        {
                            EmailAddress = dv.Table.Rows[i]["Email"].ToString();
                        }
                        if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                        {
                            Name = dv.Table.Rows[i]["Name"].ToString();
                        }
                        if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                        {
                            Surname = dv.Table.Rows[i]["Surname"].ToString();
                        }
                        if (dv.Table.Rows[i]["IsAdmin"] != DBNull.Value)
                        {
                            IsAdmin = Convert.ToBoolean(dv.Table.Rows[i]["IsAdmin"]);
                        }
                        if (dv.Table.Rows[i]["IsArtist"] != DBNull.Value)
                        {
                            IsArtist = Convert.ToBoolean(dv.Table.Rows[i]["IsArtist"]);
                        }
                        if (dv.Table.Rows[i]["Username"] != DBNull.Value)
                        {
                            Username = dv.Table.Rows[i]["Username"].ToString();
                        }
                    }

                    // Load Sessions
                    Session["FV_UserID"] = UserID.ToString();
                    Session["FV_ArtistID"] = ArtistID.ToString();
                    Session["FV_EmailAddress"] = EmailAddress;
                    Session["FV_Name"] = Name;
                    Session["FV_Surname"] = Surname;
                    Session["FV_IsAdmin"] = IsAdmin;
                    Session["FV_IsArtist"] = IsArtist;
                    Session["FV_Username"] = Username;
                }

                LoginView lv = new LoginView();
                string TempID = "";
                if (Request.QueryString["RetUrl"] != null)
                {
                    if (Request.QueryString["RetUrl"].ToString() == "1")
                    {
                        if (Request.QueryString["ArtistID"] != null)
                        {
                            TempID = Request.QueryString["ArtistID"].ToString();
                            Response.Redirect("../Admin/Approvals/Preview.aspx?ArtistID=" + TempID);
                        }
                    }
                }
                else
                {
                    if (Convert.ToBoolean(Session["FV_IsAdmin"]) == true)
                    {
                        Response.Redirect("../Admin/Menu.aspx");
                    }
                    if (Convert.ToBoolean(Session["FV_IsArtist"]) == true)
                    {
                        Response.Redirect("../Artist/Menu.aspx");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalLogin();", true);
            }
        }
    }
}