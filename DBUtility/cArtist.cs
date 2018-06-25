using System;
using System.Data;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cArtist
    {
        public static DataView List(int IsDeleted)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@IsDeleted", IsDeleted);
            dv = db.ExecuteDataView("usp_Artist_List", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByID(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_Artist_ListByID", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView IsArtitstAllowedInExhibition(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID); 
            dv = db.ExecuteDataView("usp_IsArtitstAllowedInExhibition", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView ListByIDForViewing(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_Artist_ListByIDForViewing", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByIDForEmail(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_Artist_ListByIDForEmail", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataSet Search(int IsDeleted, string Name, int ArtistTypeID,string Pattern)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@IsDeleted", IsDeleted);
            db.AddParameter("@Name", Name);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            db.AddParameter("@Pattern", Pattern);
            DataSet ds = db.ExecuteDataSet("usp_Artist_Search", System.Data.CommandType.StoredProcedure);
            return ds;
        }

        public static DataSet CreateIndex()
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataSet ds = db.ExecuteDataSet("usp_Artist_CreateIndex", System.Data.CommandType.StoredProcedure);
            return ds;
        }

        public static DataView CreateIndexAlphabets()
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            dv = db.ExecuteDataView("usp_Artist_CreateIndexAlphabets", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static string AddArtistToExhibitionNo(int artistId, int lastModifiedUser)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = "";
            db.AddParameter("@ArtistID", artistId);
            db.AddParameter("@LastModifiedUser", lastModifiedUser);
            result = Convert.ToString(db.ExecuteScalar("usp_Artist_AddArtistToExhibition", System.Data.CommandType.StoredProcedure));
            return result;
        }

        public static DataView CreateIndexSearch(string Name, int ArtistTypeID)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Name", Name);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            dv = db.ExecuteDataView("usp_Artist_CreateIndexSearch", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataSet CreateIndexSearchAlpha(string Surname)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Surname", Surname);            
            DataSet ds = db.ExecuteDataSet("usp_Artist_CreateIndexSearchAlpha", System.Data.CommandType.StoredProcedure);
            return ds;
        }

        public static DataView CreateIndexAlphabetsSearch(string Name, int ArtistTypeID)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Name", Name);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            dv = db.ExecuteDataView("usp_Artist_CreateIndexAlphabetsSearch", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static int Add(string Name, string Surname, string CV, int ArtistTypeID, string UniqueID, string LastModifiedUser)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int ArtistID  = 0;
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@CV", CV);
            db.AddParameter("@ArtistTypeID", ArtistTypeID);
            db.AddParameter("@UniqueID", UniqueID);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            ArtistID = Convert.ToInt32( db.ExecuteScalar("usp_Artist_Add", System.Data.CommandType.StoredProcedure));
            return ArtistID;
        }

        public static void Edit(int ArtistID, string Name, string Surname, string CV, int ArtistTypeID,bool IsDeleted, string UniqueID, string LastModifiedUser)
        {
            try
            {
                DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
                int i = 0;
                db.AddParameter("@ArtistID", ArtistID);
                db.AddParameter("@Name", Name);
                db.AddParameter("@Surname", Surname);
                db.AddParameter("@CV", CV);
                db.AddParameter("@ArtistTypeID", ArtistTypeID);
                db.AddParameter("@IsDeleted", IsDeleted == true ? false : true);
                db.AddParameter("@UniqueID", UniqueID);
                db.AddParameter("@LastModifiedUser", LastModifiedUser);
                string result = Convert.ToString(db.ExecuteScalar("usp_Artist_Edit", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public static string Delete(int ArtistID, string LastModifiedUser)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = "";
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            result =Convert.ToString(db.ExecuteScalar("usp_Artist_Delete", System.Data.CommandType.StoredProcedure));
            return result;
        }
        public static DataView UserDetailsByArtistId(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_Artist_UserDetailsByArtistId", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static int UpdateProfileStatus(int artistId, int status,int adminId)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistId", artistId);
            db.AddParameter("@StatusId", status);
            db.AddParameter("@AdminId", adminId);
            int result = db.ExecuteNonQuery("usp_ProfileApprovals_Approve", System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

