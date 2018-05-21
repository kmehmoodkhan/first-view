using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using FirstView.BusinessLayer;
using FirstView.Model;
using System.Text;
using FirstView.Models;

namespace FirstView
{
    public partial class PublicMaster : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private MenuModel menuModel = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FV_UserID"] != null) // User successfully logged in
            {
                login_none.Style["Display"] = "none";
                login_success.Style["Display"] = "";
                login_success_link1.InnerText = "Logged In: " + Session["FV_Name"].ToString() + " " + Session["FV_Surname"].ToString() + " | Home";

                login_success_linktop2.InnerText = "Logout";
                login_success_linktop2.HRef = "~/Users/Login?LogOut=1";

                if (Convert.ToBoolean(Session["FV_IsAdmin"]) == true)
                {
                    login_success_link1.HRef = "Admin/Menu.aspx";
                    login_success_linktop1.HRef = "Admin/Menu.aspx";
                    if (Session["FV_Name"].ToString().Length >= 8)
                    {
                        login_success_linktop1.InnerText = Session["FV_Username"].ToString().Substring(0, 5) + "...";
                        login_success_linktop1.Title = Session["FV_Username"].ToString() + " | Home";
                    }
                    else
                    {
                        login_success_linktop1.InnerText = Session["FV_Username"].ToString();
                        login_success_linktop1.Title = Session["FV_Username"].ToString() + " | Home";
                    }
                }
                else
                {
                    login_success_link1.HRef = "Artist/Menu.aspx";
                    login_success_linktop1.HRef = "Artist/Menu.aspx";
                    if (Session["FV_Username"].ToString().Length >= 8)
                    {
                        login_success_linktop1.InnerText = Session["FV_Username"].ToString().Substring(0, 5) + "...";
                        login_success_linktop1.Title = Session["FV_Username"].ToString() + " | Home";
                    }
                    else
                    {
                        login_success_linktop1.InnerText = Session["FV_Username"].ToString();
                        login_success_linktop1.Title = Session["FV_Username"].ToString() + " | Home";
                    }
                }
            }
            else {
                login_none.Style["Display"] = "none";
                login_success.Style["Display"] = "none";
            }
          
            menuModel = new MenuModel();
            menuModel.IsAdmin = false;
            menuModel.IsArtist = false;
            menuModel.IsPublic = false;
            menuModel.IsPublic = true;
            if (MenuGlobals.PublicMenu == null)
            {
                MenuGlobals.PublicMenu = cMenu.GetMenu(menuModel);
            }
            BindMenu(MenuGlobals.PublicMenu);
            login_none.Style["Display"] = "";
            login_success.Style["Display"] = "none";
            if (Request.RawUrl.ToString().IndexOf(@"/Users/Login") != -1)
            {
                login_none.Style["Display"] = "none";
                login_success.Style["Display"] = "none";
            }

            if (Request.RawUrl.ToString().IndexOf(@"/Users/Register") != -1)
            {
                login_none.Style["Display"] = "none";
                login_success.Style["Display"] = "none";
            }

            if (Request.RawUrl.ToString().IndexOf(@"/Users/ForgotPassword") != -1)
            {
                login_none.Style["Display"] = "none";
                login_success.Style["Display"] = "none";
            }
        }
        private void BindMenu(List<MenuModel> liMenu)
        {
            if (liMenu != null)
            {
                string url = string.Empty;
                StringBuilder sbMenu = new StringBuilder();
                var mainMenu = liMenu.Where(x => x.ParentId == 0).OrderBy(x => x.SortOrder);
                string HostUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["HostUrl"].ToString();
                foreach (var mainMenuItem in mainMenu)
                {
                    if (mainMenuItem.Url.ToLower().Contains("http") || mainMenuItem.Url.ToLower().Contains(":") || mainMenuItem.Url.ToLower().Contains("www"))
                    {
                        url = mainMenuItem.Url;
                        sbMenu.Append(String.Format("<li><a href='{0}' target='_blank'>{1}</a>", url, mainMenuItem.MenuTitle));
                    }
                    else
                    {
                        url = HostUrl + mainMenuItem.Url;
                        sbMenu.Append(String.Format("<li><a href='{0}'>{1}</a>", url, mainMenuItem.MenuTitle));
                    }

                    var subMenu = liMenu.Where(x => x.ParentId == mainMenuItem.Id).OrderBy(x => x.SortOrder);
                    sbMenu.Append("<ul>");
                    foreach (var subMenuItem in subMenu)
                    {
                        if (subMenuItem.Url.ToLower().Contains("http") || subMenuItem.Url.ToLower().Contains(":") || subMenuItem.Url.ToLower().Contains("www"))
                        {
                            url = subMenuItem.Url;
                            sbMenu.Append(String.Format("<li><a href='{0}' target='_blank'>{1}</a>", url, subMenuItem.MenuTitle));
                        }
                        else
                        {
                            url = HostUrl + subMenuItem.Url;
                            sbMenu.Append(String.Format("<li><a href='{0}'>{1}</a>", url, subMenuItem.MenuTitle));
                        }
                    }
                    sbMenu.Append("</ul>");
                    sbMenu.Append("</li>");
                }
                nav.InnerHtml = sbMenu.ToString();
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}