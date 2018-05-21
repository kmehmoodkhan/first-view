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
    public partial class MenuList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeMenus();
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
            MenuModel menuModel = new MenuModel();
            if (ddlMenuCategory.SelectedIndex > 0)
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
                if (ddlMenu.SelectedItem.Value == "--All--")
                {
                    menuModel.ParentId = 0;
                }
                else
                {
                    menuModel.ParentId = Convert.ToInt32(ddlMenu.SelectedItem.Value);
                }
                List<MenuModel> listMenu = cMenu.GetSubMenuList(menuModel);

                if (listMenu != null && listMenu.Count > 0)
                {
                    gvMenu.DataSource = listMenu;
                    gvMenu.DataBind();
                    gvMenu.Visible = true;
                    lblStatus.Visible = false;
                }
                else
                {
                    gvMenu.Visible = false;
                    lblStatus.Text = "There is no data available.";
                    lblStatus.Visible = true;
                }
            }
        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddMenu.aspx", true);
        }

        protected void butSearch_ServerClick(object sender, EventArgs e)
        {
            DoSearch();
        }

        protected void butClearSearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("MenuList.aspx", true);
        }
        protected void gvMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            MenuModel MenuModel = new MenuModel();
            if (gvMenu.PageIndex > 0)
            {
                index = Convert.ToInt32(e.CommandArgument) - (gvMenu.PageIndex * gvMenu.PageSize);
            }
            else
            {
                index = Convert.ToInt32(e.CommandArgument);
            }
            MenuModel.Id = Convert.ToInt32(gvMenu.DataKeys[index]["Id"]);
            if (e.CommandName.Equals("deleteRecord"))
            {
                int result = cMenu.DeleteMenu(MenuModel);
                DoSearch();
                InitializeMenus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
            }
            else if (e.CommandName.Equals("editRecord"))
            {
                Response.Redirect("AddMenu.aspx?Id=" + MenuModel.Id);
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
        protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
            ddlMenu.Items.Insert(0, "--All--");
            DoSearch();
        }
    }
}