using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Model;

namespace FirstView.Admin.MenuManagement
{
    public partial class AddPageUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPageTitle.Focus();
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    BindPageDetails(id);
                }
            }
            else
            {
                txtPageTitle.Focus();
            }
        }

        private void BindPageDetails(int id)
        {
            PageUrlsModel pageUrls = new PageUrlsModel();
            pageUrls.Id = id;
            List<Entities.PageUrls> listPageUrls = cPageUrls.GetPageUrls(pageUrls);
            if (listPageUrls != null && listPageUrls.Count > 0)
            {
                txtPageTitle.Text = listPageUrls[0].PageTitle;
                lblPageUrl.Text = listPageUrls[0].PageUrl;
                ViewState["Id"] = id;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string pageUrl = string.Empty;
            int id = 0;
            if (fpFileUpload.HasFile)
            {
                string filename = Path.GetFileName(fpFileUpload.FileName);
                fpFileUpload.SaveAs(Server.MapPath("~/Other/") + filename);
                pageUrl = "Other/" + filename;
            }
            else
            {
                pageUrl = lblPageUrl.Text.Trim();
            }
            if (!string.IsNullOrEmpty(pageUrl) && !string.IsNullOrEmpty(txtPageTitle.Text.Trim()))
            {
                id = ViewState["Id"] == null ? 0 : Convert.ToInt32(ViewState["Id"]);
               
                PageUrlsModel pageUrlsModel = new PageUrlsModel();
                pageUrlsModel.Id = id;
                pageUrlsModel.PageTitle = txtPageTitle.Text.Trim();
                pageUrlsModel.PageUrl = pageUrl;
                pageUrlsModel.CreatedBy = Convert.ToInt32(Session["FV_UserID"]);
                string result = cPageUrls.SaveUpdatePageUrls(pageUrlsModel);
                pmesage.Attributes["class"] = "text-success";
                lblMessage.Text = result;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                if(string.IsNullOrEmpty(txtPageTitle.Text.Trim()))
                {
                    lblMessage.Text = "Menu title is  mandatory field.";
                }else if (string.IsNullOrEmpty(pageUrl.Trim()))
                {
                    lblMessage.Text = "Either upload a file or input the url in page url input box.";
                }
                pmesage.Attributes["class"] = "text-danger";
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
    }
}