using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FirstView.BusinessLayer;
using FirstView.Model;
using FirstView.Models;

namespace FirstView.Admin.MenuManagement
{
    public partial class AddMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPageDropdown();
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    BindPageDetails(id);
                }
            }
        }

        private void BindPageDetails(int id)
        {
            MenuModel menuModel = new MenuModel();
            menuModel.Id = id;
            List<MenuModel> li = cMenu.GetMenu(menuModel);
            if (li != null)
            {
                menuModel.Id = 0;
                if (li[0].IsAdmin == true)
                {
                    menuModel.IsAdmin = true;
                    ddlMenuCategory.SelectedValue = "3";
                }
                else if (li[0].IsArtist == true)
                {
                    menuModel.IsArtist = true;
                    ddlMenuCategory.SelectedValue = "2";
                }
                else if (li[0].IsPublic == true)
                {
                    menuModel.IsPublic = true;
                    ddlMenuCategory.SelectedValue = "1";
                }
                BindDropDownList(menuModel);
                txtMenuTitle.Text = li[0].MenuTitle;
                txtSortOrder.Text = Convert.ToString(li[0].SortOrder);
                var parentMenu = Convert.ToString(li[0].ParentId);
                ddlMenu.SelectedValue = string.IsNullOrEmpty(parentMenu) ? "0" : parentMenu;
                ddlPages.SelectedValue = Convert.ToString(li[0].PageId);
                ViewState["Id"] = id;
            }
        }

        private void BindPageDropdown()
        {
            PageUrlsModel pageUrlsModel = new PageUrlsModel();
            List<FirstView.Entities.PageUrls> listPages = cPageUrls.GetPageUrls(pageUrlsModel);
            if (listPages != null)
            {
                ddlPages.DataTextField = "PageTitle";
                ddlPages.DataValueField = "Id";
                ddlPages.DataSource = listPages;
                ddlPages.DataBind();
                ddlPages.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MenuModel menuModel = new MenuModel();
            if (ddlMenuCategory.SelectedIndex > 0 && ddlPages.SelectedIndex > 0 && !string.IsNullOrEmpty(txtMenuTitle.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["Id"])))
                {
                    menuModel.Id = Convert.ToInt32(ViewState["Id"]);
                }
               
                menuModel.ParentId = Convert.ToInt32((ddlMenu.SelectedItem.Value == "--Select--" || ddlMenu.SelectedItem.Value == "--All--") ? "0" : ddlMenu.SelectedItem.Value);
                if (ddlMenuCategory.SelectedItem.Value == "1")
                {
                    menuModel.IsPublic = true;
                }
                else if (ddlMenuCategory.SelectedItem.Value == "2")
                {
                    menuModel.IsArtist = true;
                }
                else if (ddlMenuCategory.SelectedItem.Value == "3")
                {
                    menuModel.IsAdmin = true;
                }
                menuModel.CreatedBy = Convert.ToInt32(Session["FV_UserID"]);
                menuModel.MenuTitle = txtMenuTitle.Text.Trim();
                menuModel.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
                PageUrlsModel pageUrlsModel = new PageUrlsModel();
                pageUrlsModel.Id = Convert.ToInt32(ddlPages.SelectedItem.Value);
                menuModel.PageId = pageUrlsModel.Id;
                List<FirstView.Entities.PageUrls> listPages = cPageUrls.GetPageUrls(pageUrlsModel);
                if (listPages != null)
                {
                    menuModel.Url = listPages[0].PageUrl;
                }
                string result = cMenu.SaveUpdateMenu(menuModel);
                pmesage.Attributes["class"] = "text-success";
                lblMessage.Text = result;
                InitializeMenus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                pmesage.Attributes["class"] = "text-danger";
                if (ddlMenuCategory.SelectedIndex <= 0)
                {
                    lblMessage.Text = "Please select a valid menu cateogry";
                }
               else if (string.IsNullOrEmpty(txtMenuTitle.Text.Trim()))
                {
                    lblMessage.Text = "Please input a valid menu title";
                }
                else if (string.IsNullOrEmpty(txtSortOrder.Text.Trim()))
                {
                    lblMessage.Text = "Please input a valid integer value in sort order";
                }
                else if (ddlPages.SelectedIndex<=0)
                {
                    lblMessage.Text = "Please select page to be linked in menu from Linked Page dropdown";
                }
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        private void InitializeMenus()
        {
            MenuModel menuModel = new MenuModel();
            menuModel.IsAdmin = true;
            menuModel.IsPublic = false;
            menuModel.IsArtist = false;
            MenuGlobals.AdminMenu = cMenu.GetMenu(menuModel);
            menuModel.IsAdmin = false;
            menuModel.IsPublic = false;
            menuModel.IsArtist = true;
            MenuGlobals.ArtistMenu = cMenu.GetMenu(menuModel);
            menuModel.IsAdmin = false;
            menuModel.IsArtist = false;
            menuModel.IsPublic = true;
            MenuGlobals.PublicMenu = cMenu.GetMenu(menuModel);
        }
        protected void ddlMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuModel menuModel = new MenuModel();
            if (ddlMenuCategory.SelectedIndex != 0)
            {
                if (ddlMenuCategory.SelectedItem.Value == "1")
                {
                    menuModel.IsPublic = true;
                }
                else if (ddlMenuCategory.SelectedItem.Value == "2")
                {
                    menuModel.IsArtist = true;
                }
                else if (ddlMenuCategory.SelectedItem.Value == "3")
                {
                    menuModel.IsAdmin = true;
                }
                BindDropDownList(menuModel);
            }
        }
        public void BindDropDownList(MenuModel menuModel)
        {
            List<MenuModel> list = cMenu.GetParentMenuList(menuModel);
            ddlMenu.DataTextField = "MenuTitle";
            ddlMenu.DataValueField = "Id";
            ddlMenu.DataSource = list;
            ddlMenu.DataBind();
            ddlMenu.Items.Insert(0, "--Select--");
        }
    }
}