using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Model;

namespace FirstView.Admin.MenuManagement
{
    public partial class PagesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserValidity();
            if (!Page.IsPostBack)
            {
                DoSearch();
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
        private void DoSearch()
        {
            PageUrlsModel pageUrls = new PageUrlsModel();
            if (txtPageTitle.Text.Length > 0)
            {
                pageUrls.PageTitle = txtPageTitle.Text.Trim().Replace("'", "''");
            }

            List<Entities.PageUrls> listPageUrls = cPageUrls.GetPageUrls(pageUrls);

            if (listPageUrls != null && listPageUrls.Count > 0)
            {
                gvPageUrls.DataSource = listPageUrls;
                gvPageUrls.DataBind();
                gvPageUrls.Visible = true;
                lblStatus.Visible = false;
            }
            else
            {
                gvPageUrls.Visible = false;
                lblStatus.Text = "There is no data available.";
                lblStatus.Visible = true;
            }
        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPageUrl.aspx", true);
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("AddPageUrl.aspx", true);
        }
        protected void gvPageUrls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            PageUrlsModel pageUrlsModel = new PageUrlsModel();
            if (gvPageUrls.PageIndex > 0)
            {
                index = Convert.ToInt32(e.CommandArgument) - (gvPageUrls.PageIndex * gvPageUrls.PageSize);
            }
            else
            {
                index = Convert.ToInt32(e.CommandArgument);
            }
            pageUrlsModel.Id = Convert.ToInt32(gvPageUrls.DataKeys[index]["Id"]);
            if (e.CommandName.Equals("deleteRecord"))
            {
                int result = cPageUrls.DeletePageUrls(pageUrlsModel);
                DoSearch();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
            }
            else if (e.CommandName.Equals("editRecord"))
            {
                Response.Redirect("AddPageUrl.aspx?Id=" + pageUrlsModel.Id);
            }
        }

        protected void gvPageUrls_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}