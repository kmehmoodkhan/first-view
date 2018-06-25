using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using System.Linq;

namespace FirstView.Artist
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDropDowns();
            }
            if (Request.QueryString["sAlpha"] != null)
            {
                DoSearchAlphabets();
            }
            else
            {
                LoadData();
            }
        }

        private void LoadDropDowns()
        {
            cArtistType at = new cArtistType();
            DataView dv3 = new DataView();

            dv3 = at.List();
            if (dv3.Table.Rows.Count > 0)
            {
                ListItem li1 = new ListItem();
                li1.Text = "--Select All--";
                li1.Value = "";
                ddlArtistType.Items.Add(li1);
                for (int i = 0; i < dv3.Table.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = dv3.Table.Rows[i]["Description"].ToString();
                    li.Value = dv3.Table.Rows[i]["ArtistTypeID"].ToString();
                    ddlArtistType.Items.Add(li);
                }
            }
        }

        private void LoadData()
        {
            string Surname = "";
            string Name = "";
            int ArtistID = 0;
            int IsHeader = 0;
            bool HasData = false;
            string CV = "";
            string TrimCV = "";
            string ArtistType = "";
            string ImageFileName = "";

            cArtist art = new cArtist();

            DataView dv2 = new DataView();
            StringBuilder sbHTML = new StringBuilder();

            dv2 = art.CreateIndexAlphabets();
            if (dv2.Table.Rows.Count > 0)
            {
                rptAlphabets.DataSource = dv2.Table;
                rptAlphabets.DataBind();
            }


            DataSet ds = art.CreateIndex();
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasData = true;
                sbHTML.Clear();
                sbHTML.AppendLine("<ul class='list-group'>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int artistId = 0;

                    if (ds.Tables[0].Rows[i]["artistId"] != DBNull.Value)
                    {
                        artistId = Convert.ToInt32(ds.Tables[0].Rows[i]["artistId"].ToString());
                    }

                    if (ds.Tables[0].Rows[i]["Name"] != DBNull.Value)
                    {
                        Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    }
                    else
                    {
                        Name = "";
                    }
                    if (ds.Tables[0].Rows[i]["Surname"] != DBNull.Value)
                    {
                        Surname = ds.Tables[0].Rows[i]["Surname"].ToString();
                    }
                    else
                    {
                        Surname = "";
                    }
                    if (ds.Tables[0].Rows[i]["ArtistType"] != DBNull.Value)
                    {
                        ArtistType = ds.Tables[0].Rows[i]["ArtistType"].ToString();
                    }
                    else
                    {
                        ArtistType = "";
                    }
                    if (ds.Tables[0].Rows[i]["ImageFileName"] != DBNull.Value)
                    {
                        ImageFileName = ds.Tables[0].Rows[i]["ImageFileName"].ToString();
                    }
                    else
                    {
                        ImageFileName = "blank50.png";
                    }
                    ArtistID = Convert.ToInt32(ds.Tables[0].Rows[i]["ArtistID"]);
                    IsHeader = Convert.ToInt32(ds.Tables[0].Rows[i]["IsHeader"]);
                    if (ds.Tables[0].Rows[i]["CV"] != null)
                    {
                        CV = ds.Tables[0].Rows[i]["CV"].ToString();
                    }
                    if (CV.Length > 150)
                    {
                        TrimCV = CV.Substring(0, 147) + "...";
                    }
                    else
                    {
                        TrimCV = CV;
                    }

                    if (IsHeader == 1)
                    {
                        sbHTML.AppendLine("<li class='list-group-item active' style='height: 30px; padding: 5px;'>" + Surname.Substring(0, 1).ToUpper() + "</li>");
                    }
                    else
                    {
                        string url = "IndexView.aspx?ArtistID=" + ArtistID.ToString();

                        DataView dv = ds.Tables[1].DefaultView;
                        dv.RowFilter = $"ArtistId={artistId}";


                        List<string> images = new List<string>();

                        if (dv.Table.Rows.Count > 0)    
                        {
                            foreach(DataRow dr in dv.ToTable().Rows)
                            {
                                if(dr["ImageFileName"] != null)
                                {
                                    images.Add(Convert.ToString(dr["ImageFileName"]));
                                }
                            }
                        }

                        string imagesList = "<p class='col-md-4'>";

                        for (int j = 0; j < images.Count; j++)
                        {
                            imagesList += "<img class='img-thumbnail-mini' src='/Uploads/Preview/" + images[j] + "'></img>";
                        }
                        imagesList += "</p>";

                        sbHTML.AppendLine("<li class='list-group-item' onclick='return ShowArtistDetail(" + ArtistID + ")'>");
                        sbHTML.AppendLine("<div class='row'>");
                        sbHTML.AppendLine("<a class='col-md-2' href='IndexView.aspx?ArtistID=" + ArtistID.ToString() + "'>" + Name + " " + Surname + "<br/><p class='text-muted'>" + ArtistType + "</p></a>");
                        //sbHTML.AppendLine("<p class='col-md-2'><img src='../Uploads/Original/" + ImageFileName + "' alt='' class='img-thumbnail'></p>");
                       

                        sbHTML.Append(imagesList);

                        sbHTML.AppendLine("<p class='col-md-6'>" + TrimCV + "</p>");
                        sbHTML.AppendLine("</div>");
                        sbHTML.AppendLine("</li>");
                    }
                }
                sbHTML.AppendLine("</ul>");
            }
            artistindex.InnerHtml = sbHTML.ToString();

            if (HasData == true)
            {
                lblStatus.Visible = false;
                rptAlphabets.Visible = true;
                artistindex.Style["Display"] = "";
            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = "There is no data available.";
                rptAlphabets.Visible = false;
                artistindex.Style["Display"] = "none";
            }
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            int ArtistTypeID = 0;
            string Name = "";
            string Surname = "";
            int ArtistID = 0;
            int IsHeader = 0;
            string CV = "";
            string TrimCV = "";
            string ArtistType = "";
            cArtist art = new cArtist();
            DataView dv = new DataView();
            DataView dv2 = new DataView();
            bool HasData = false;
            string ImageFileName = "";

            StringBuilder sbHTML = new StringBuilder();

            if (txtSearchName.Text.Length > 0)
            {
                Name = txtSearchName.Text.Trim().Replace("'", "''");
            }
            if (ddlArtistType.SelectedValue != null)
            {
                if (ddlArtistType.SelectedValue.Length > 0)
                {
                    ArtistTypeID = Convert.ToInt32(ddlArtistType.SelectedValue);
                }
            }

            dv2 = art.CreateIndexAlphabetsSearch(Name, ArtistTypeID);
            if (dv2.Table.Rows.Count > 0)
            {
                rptAlphabets.DataSource = dv2.Table;
                rptAlphabets.DataBind();
            }

            dv = art.CreateIndexSearch(Name, ArtistTypeID);
            if (dv.Table.Rows.Count > 0)
            {
                HasData = true;
                sbHTML.Clear();
                sbHTML.AppendLine("<ul class='list-group'>");
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["Name"] != DBNull.Value)
                    { Name = dv.Table.Rows[i]["Name"].ToString(); }
                    else { Name = ""; }
                    if (dv.Table.Rows[i]["Surname"] != DBNull.Value)
                    { Surname = dv.Table.Rows[i]["Surname"].ToString(); }
                    else { Surname = ""; }
                    if (dv.Table.Rows[i]["ArtistType"] != DBNull.Value)
                    { ArtistType = dv.Table.Rows[i]["ArtistType"].ToString(); }
                    else { ArtistType = ""; }
                    if (dv.Table.Rows[i]["ImageFileName"] != DBNull.Value)
                    { ImageFileName = dv.Table.Rows[i]["ImageFileName"].ToString(); }
                    else
                    { ImageFileName = "blank50.png"; }
                    ArtistID = Convert.ToInt32(dv.Table.Rows[i]["ArtistID"]);
                    IsHeader = Convert.ToInt32(dv.Table.Rows[i]["IsHeader"]);
                    if (dv.Table.Rows[i]["CV"] != null)
                    { CV = dv.Table.Rows[i]["CV"].ToString(); }

                    if (CV.Length > 150)
                    { TrimCV = CV.Substring(0, 147) + "..."; }
                    else
                    { TrimCV = CV; }

                    if (IsHeader == 1)
                    {
                        sbHTML.AppendLine("<li class='list-group-item active' style='height: 30px; padding: 5px;'>" + Surname.Substring(0, 1).ToUpper() + "</li>");
                        //sbHTML.AppendLine("<li class='list-group-item'>");
                        //sbHTML.AppendLine("<div class='row'>");
                        //sbHTML.AppendLine("<a class='col-md-2' href='IndexView.aspx?ArtistID=" + ArtistID.ToString() + "'>" + Name + " " + Surname + "<br/><p class='text-muted'>" + ArtistType + "</p></a>");
                        //sbHTML.AppendLine("<p class='col-md-2'><img src='../Uploads/Preview/" + ImageFileName + "' alt='' class='img-thumbnail'></p>");
                        //sbHTML.AppendLine("<p class='col-md-8'>" + TrimCV + "</p>");
                        //sbHTML.AppendLine("</div>");
                        //sbHTML.AppendLine("</li>");
                    }
                    else
                    {
                        string url = "IndexView.aspx?ArtistID=" + ArtistID.ToString();

                        sbHTML.AppendLine("<li class='list-group-item'>");
                        sbHTML.AppendLine("<div class='row'>");
                        sbHTML.AppendLine("<a class='col-md-2' href='" + url + "'>" + Name + " " + Surname + "<br/><p class='text-muted'>" + ArtistType + "</p></a>");
                        sbHTML.AppendLine("<p class='col-md-2'><img src='../Uploads/Preview/" + ImageFileName + "' alt='' class='img-thumbnail'></p>");
                        sbHTML.AppendLine("<p class='col-md-8'>" + TrimCV + "</p>");
                        sbHTML.AppendLine("</div>");
                        sbHTML.AppendLine("</li>");
                    }
                }
                sbHTML.AppendLine("</ul>");
            }
            artistindex.InnerHtml = sbHTML.ToString();

            if (HasData == true)
            {
                lblStatus.Visible = false;
                rptAlphabets.Visible = true;
                artistindex.Style["Display"] = "";
            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = "There is no data available.";
                rptAlphabets.Visible = false;
                artistindex.Style["Display"] = "none";
            }
        }

        private void DoSearchAlphabets()
        {
            string Name = "";
            string Surname = "";
            int ArtistID = 0;
            string CV = "";
            string TrimCV = "";
            string ArtistType = "";
            cArtist art = new cArtist();
            
            DataView dv2 = new DataView();
            bool HasData = false;
            string SearchAlphabet = "";
            string ImageFileName = "";

            StringBuilder sbHTML = new StringBuilder();

            if (Request.QueryString["sAlpha"] != null)
            {
                SearchAlphabet = Request.QueryString["sAlpha"].ToString();
            }

            dv2 = art.CreateIndexAlphabets();
            if (dv2.Table.Rows.Count > 0)
            {
                rptAlphabets.DataSource = dv2.Table;
                rptAlphabets.DataBind();
            }

            DataSet ds = art.CreateIndexSearchAlpha(SearchAlphabet);
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasData = true;
                sbHTML.Clear();
                sbHTML.AppendLine("<ul class='list-group'>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int artistId = Convert.ToInt32(ds.Tables[0].Rows[i]["ArtistId"]);

                    if (ds.Tables[0].Rows[i]["Name"] != DBNull.Value)
                    { Name = ds.Tables[0].Rows[i]["Name"].ToString(); }
                    else
                    { Name = ""; }
                    if (ds.Tables[0].Rows[i]["Surname"] != DBNull.Value)
                    { Surname = ds.Tables[0].Rows[i]["Surname"].ToString(); }
                    else { Surname = ""; }
                    if (ds.Tables[0].Rows[i]["ArtistType"] != DBNull.Value)
                    { ArtistType = ds.Tables[0].Rows[i]["ArtistType"].ToString(); }
                    else { ArtistType = ""; }
                    if (ds.Tables[0].Rows[i]["ImageFileName"] != DBNull.Value)
                    { ImageFileName = ds.Tables[0].Rows[i]["ImageFileName"].ToString(); }
                    else
                    { ImageFileName = "blank50.png"; }
                    ArtistID = Convert.ToInt32(ds.Tables[0].Rows[i]["ArtistID"]);

                    if (ds.Tables[0].Rows[i]["CV"] != null)
                    { CV = ds.Tables[0].Rows[i]["CV"].ToString(); }

                    if (CV.Length > 150)
                    { TrimCV = CV.Substring(0, 147) + "..."; }
                    else
                    { TrimCV = CV; }

                    string url = "IndexView.aspx?ArtistID=" + ArtistID.ToString();

                    if (i == 0)
                    {
                        DataView dv = ds.Tables[1].DefaultView;
                        dv.RowFilter = $"ArtistId={artistId}";


                        List<string> images = new List<string>();

                        if (dv.Table.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dv.ToTable().Rows)
                            {
                                if (dr["ImageFileName"] != null)
                                {
                                    images.Add(Convert.ToString(dr["ImageFileName"]));
                                }
                            }
                        }

                        string imagesList = "<p class='col-md-4'>";

                        for (int j = 0; j < images.Count; j++)
                        {
                            imagesList += "<img class='img-thumbnail-mini' src='/Uploads/Preview/" + images[j] + "'></img>";
                        }
                        imagesList += "</p>";

                        sbHTML.AppendLine("<li class='list-group-item active' style='height: 30px; padding: 5px;'>" + Surname.Substring(0, 1).ToUpper() + "</li>");
                        sbHTML.AppendLine("<li class='list-group-item'>");
                        sbHTML.AppendLine("<div class='row'>");
                        sbHTML.AppendLine("<a class='col-md-2' href='IndexView.aspx?ArtistID=" + ArtistID.ToString() + "'>" + Name + " " + Surname + "<br/><p class='text-muted'>" + ArtistType + "</p></a>");
                        // sbHTML.AppendLine("<p class='col-md-2'><img src='../Uploads/Preview/" + ImageFileName + "' alt='' class='img-thumbnail'></p>");
                        sbHTML.Append(imagesList);
                        sbHTML.AppendLine("<p class='col-md-6'>" + TrimCV + "</p>");
                        sbHTML.AppendLine("</div>");
                        sbHTML.AppendLine("</li>");
                    }
                    else
                    {
                        DataView dv = ds.Tables[1].DefaultView;
                        dv.RowFilter = $"ArtistId={artistId}";


                        List<string> images = new List<string>();

                        if (dv.Table.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dv.ToTable().Rows)
                            {
                                if (dr["ImageFileName"] != null)
                                {
                                    images.Add(Convert.ToString(dr["ImageFileName"]));
                                }
                            }
                        }

                        string imagesList = "<p class='col-md-4'>";

                        for (int j = 0; j < images.Count; j++)
                        {
                            imagesList += "<img class='img-thumbnail-mini' src='/Uploads/Preview/" + images[j] + "'></img>";
                        }
                        imagesList += "</p>";


                        sbHTML.AppendLine("<li class='list-group-item'>");
                        sbHTML.AppendLine("<div class='row'>");
                        sbHTML.AppendLine("<a class='col-md-2' href='IndexView.aspx?ArtistID=" + ArtistID.ToString() + "'>" + Name + " " + Surname + "<br/><p class='text-muted'>" + ArtistType + "</p></a>");

                        sbHTML.Append(imagesList);

                        sbHTML.AppendLine("<p class='col-md-6'>" + TrimCV + "</p>");
                        sbHTML.AppendLine("</div>");
                        sbHTML.AppendLine("</li>");
                    }
                }
                sbHTML.AppendLine("</ul>");
            }
            artistindex.InnerHtml = sbHTML.ToString();

            if (HasData == true)
            {
                lblStatus.Visible = false;
                rptAlphabets.Visible = true;
                artistindex.Style["Display"] = "";
            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = "There is no data available.";
                rptAlphabets.Visible = false;
                artistindex.Style["Display"] = "none";
            }
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            txtSearchName.Text = "";
            LoadData();
        }

        protected void ddlArtistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSearch();
        }


    }
}