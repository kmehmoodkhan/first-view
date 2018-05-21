using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstView.Model;
using MSSQL.DBHelper;

namespace FirstView.DataAccessLayer
{
    public class dPageUrls
    {
        public static List<Entities.PageUrls> GetPageUrls(PageUrlsModel pageUrlsModel)
        {
            List<Entities.PageUrls> liMenu = null;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", pageUrlsModel.Id);
            db.AddParameter("@PageTitle", pageUrlsModel.PageTitle);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("uspGETPageUrls", System.Data.CommandType.StoredProcedure);
            if (dv != null)
            {
                liMenu = GenericConversion.Cast.ToGenericList<Entities.PageUrls>(dv.ToTable(), true);
            }
            return liMenu;
        }

        public static int DeletePageUrls(PageUrlsModel pageUrlsModel)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", pageUrlsModel.Id);
            db.AddParameter("@CreatedBy", pageUrlsModel.CreatedBy);
            i = Convert.ToInt32(db.ExecuteScalar("uspDeletePageUrl", System.Data.CommandType.StoredProcedure));
            return i;
        }

        public static string SaveUpdatePageUrls(PageUrlsModel pageUrlsModel)
        {
            string result = string.Empty;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Id", pageUrlsModel.Id);
            db.AddParameter("@PageTitle", pageUrlsModel.PageTitle);
            db.AddParameter("@PageUrl", pageUrlsModel.PageUrl);
            db.AddParameter("@CreatedBy", pageUrlsModel.CreatedBy);
            result = Convert.ToString(db.ExecuteScalar("uspSaveUpdatePageUrls", System.Data.CommandType.StoredProcedure));
            return result;
        }
    }
}
