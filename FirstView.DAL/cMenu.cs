using System;
using System.Collections.Generic;
using System.Data;

using FirstView.Model;

using MSSQL.DBHelper;
namespace FirstView.BusinessLayer
{
    public class cMenu
    {
        public static List<MenuModel> GetMenu(MenuModel menuModel)
        {
            return FirstView.DataAccessLayer.cMenu.GetMenu(menuModel);
        }
       
        public static string SaveUpdateMenu(MenuModel menuModel)
        {
            return FirstView.DataAccessLayer.cMenu.SaveUpdateMenu(menuModel);
        }
        public static List<MenuModel> GetParentMenuList(MenuModel menuModel)
        {
            return FirstView.DataAccessLayer.cMenu.GetParentMenuList(menuModel);
        }
        public static List<MenuModel> GetSubMenuList(MenuModel menuModel)
        {
            return FirstView.DataAccessLayer.cMenu.GetSubMenuList(menuModel);
        }
        public static int DeleteMenu(MenuModel menuModel)
        {
            return FirstView.DataAccessLayer.cMenu.DeleteMenu(menuModel);
        }
    }
}
