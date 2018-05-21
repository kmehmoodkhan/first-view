using System.Data;
using System.Data.Common;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cTempImages
    {
        public static bool CheckImageExists(string UniqueID)
        {
            bool Exists = false;
            DbDataReader reader;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@UniqueID", UniqueID);
            reader = db.ExecuteReader("usp_TempImages_ListByUniqueID", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            return Exists;
        }

        public static void Add(string OriginalFileName, string NewFileName, string UniqueID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int i = 0;
            db.AddParameter("@OriginalFileName", OriginalFileName);
            db.AddParameter("@NewFileName", NewFileName);
            db.AddParameter("@UniqueID", UniqueID);
            i = db.ExecuteNonQuery("usp_TempImages_Add", System.Data.CommandType.StoredProcedure);
        }

    }
}

