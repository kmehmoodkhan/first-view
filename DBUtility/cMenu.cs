using System;
using System.Collections.Generic;
using System.Data;

using FirstView.Model;

using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cMenu
    {
        public static List<MenuModel> GetMenu(MenuModel menuModel)
        {
            List<MenuModel> liMenu = null;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", menuModel.Id);
            db.AddParameter("@IsAdmin", menuModel.IsAdmin);
            db.AddParameter("@IsArtist", menuModel.IsArtist);
            db.AddParameter("@IsPublic", menuModel.IsPublic);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_GetMenu", System.Data.CommandType.StoredProcedure);
            if (dv != null)
            {
                liMenu = GenericConversion.Cast.ToGenericList<MenuModel>(dv.ToTable(), true);
            }
            return liMenu;
        }

        public static List<MenuModel> GetSubMenuList(MenuModel menuModel)
        {
            List<MenuModel> liMenu = null;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ParentId", menuModel.ParentId);
            db.AddParameter("@IsAdmin", menuModel.IsAdmin);
            db.AddParameter("@IsArtist", menuModel.IsArtist);
            db.AddParameter("@IsPublic", menuModel.IsPublic);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_GetSubMenuList", System.Data.CommandType.StoredProcedure);
            if (dv != null)
            {
                liMenu = GenericConversion.Cast.ToGenericList<MenuModel>(dv.ToTable(), true);
            }
            return liMenu;
        }

        public static List<MenuModel> GetParentMenuList(MenuModel menuModel)
        {
            List<MenuModel> liMenu = null;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);            
            db.AddParameter("@IsAdmin", menuModel.IsAdmin);
            db.AddParameter("@IsArtist", menuModel.IsArtist);
            db.AddParameter("@IsPublic", menuModel.IsPublic);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_GetParentMenuList", System.Data.CommandType.StoredProcedure);
            if (dv != null)
            {
                liMenu = GenericConversion.Cast.ToGenericList<MenuModel>(dv.ToTable(), true);
            }
            return liMenu;
        }

        public static string SaveUpdateMenu(MenuModel menuModel)
        {
            string i = string.Empty;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", menuModel.Id);
            db.AddParameter("@ParentId", menuModel.ParentId);
            db.AddParameter("@PageId", menuModel.PageId);
            db.AddParameter("@MenuTitle", menuModel.MenuTitle);
            db.AddParameter("@Url", menuModel.Url);
            db.AddParameter("@IsAdmin", menuModel.IsAdmin);
            db.AddParameter("@IsArtist", menuModel.IsArtist);
            db.AddParameter("@IsPublic", menuModel.IsPublic);
            db.AddParameter("@IsActive", menuModel.IsActive);
            db.AddParameter("@SortOrder", menuModel.SortOrder);
            db.AddParameter("@CreatedBy", menuModel.CreatedBy);
            i = Convert.ToString(db.ExecuteScalar("usp_SaveUpdateMenu", System.Data.CommandType.StoredProcedure));
            return i;
        }
        public static int DeleteMenu(MenuModel menuModel)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", menuModel.Id);
            db.AddParameter("@CreatedBy", menuModel.CreatedBy);
            i = Convert.ToInt32(db.ExecuteScalar("usp_DeleteMenu", System.Data.CommandType.StoredProcedure));
            return i;
        }
    }
}
