using System;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cSettings
    {
        public static void UpdateByID(int SettingID, string SettingValue)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int i = 0;
            db.AddParameter("@SettingID", SettingID);
            db.AddParameter("@SettingValue", SettingValue);
            i  = db.ExecuteNonQuery("usp_Setting_UpdateByID", System.Data.CommandType.StoredProcedure);
        }

        public static void UpdateByCode(string SettingCode, string SettingValue)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int i = 0;
            db.AddParameter("@SettingCode", SettingCode);
            db.AddParameter("@SettingValue", SettingValue);
            i = db.ExecuteNonQuery("usp_Setting_UpdateByCode", System.Data.CommandType.StoredProcedure);
        }


        public  static string ListByCode(string SettingCode)
        {
            string Result = "";
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@SettingCode", SettingCode);
            Result = db.ExecuteScalar("usp_Setting_ListByCode", System.Data.CommandType.StoredProcedure).ToString();
            return Result;
        }
        public static string LogException(Exception ex)
        {
            string Result = "";
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Data", string.Empty);
            db.AddParameter("@HelpLink", ex.HelpLink);
            db.AddParameter("@HResult",Convert.ToString(ex.HResult));
            db.AddParameter("@InnerException", Convert.ToString(ex.InnerException));
            db.AddParameter("@Message", ex.Message);
            db.AddParameter("@Source", ex.Source);
            db.AddParameter("@StackTrace", ex.StackTrace);
            db.AddParameter("@TargetSite", Convert.ToString(ex.TargetSite));
            Result = db.ExecuteScalar("usp_InsertWebsiteErrors", System.Data.CommandType.StoredProcedure).ToString();
            return Result;
        }
    }
}

