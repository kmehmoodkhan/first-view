using System.Data;
using System.Data.Common;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cArtistType
    {
        public static void Add(string Description)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Description", Description);
            i = db.ExecuteNonQuery("usp_ArtistType_Add", System.Data.CommandType.StoredProcedure);
        }

        public static void Edit(int ArtistTypeID, string Description)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            db.AddParameter("@Description", Description);
            i = db.ExecuteNonQuery("usp_ArtistType_Edit", System.Data.CommandType.StoredProcedure);
        }

        public static void Delete(int ArtistTypeID)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            i = db.ExecuteNonQuery("usp_ArtistType_Delete", System.Data.CommandType.StoredProcedure);
        }

        public static bool CanDelete(int ArtistTypeID)
        {
            bool CanDel = true;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DbDataReader reader;
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            reader = db.ExecuteReader("usp_ArtistType_DeleteCheck", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                CanDel = false;
            }
            reader.Close();
            reader.Dispose();
            return CanDel;
        }

        public static bool CheckExistsAdd(string Description)
        {
            bool Exists = false;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DbDataReader reader;
            db.AddParameter("@Description", Description);
            reader = db.ExecuteReader("usp_ArtistType_CheckExistsAdd", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            reader.Close();
            reader.Dispose();
            return Exists;
        }

        public static bool CheckExistsEdit(int ArtistTypeID,string Description)
        {
            bool Exists = false;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DbDataReader reader;
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            db.AddParameter("@Description", Description);
            reader = db.ExecuteReader("usp_ArtistType_CheckExistsEdit", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            reader.Close();
            reader.Dispose();
            return Exists;
        }

        public static DataView ListByID(int ArtistTypeID)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            dv = db.ExecuteDataView("usp_ArtistType_ListByID", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView Search(string Description)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Description", Description);
            dv = db.ExecuteDataView("usp_ArtistType_Search", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView List()
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            dv = db.ExecuteDataView("usp_ArtistType_List", System.Data.CommandType.StoredProcedure);
            return dv;
        }

    }
}

