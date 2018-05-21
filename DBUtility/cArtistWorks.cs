using System;
using System.Data;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cArtistWorks
    {
        public static DataView ListByArtistID(int ArtistID, int IsDeleted)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@IsDeleted", IsDeleted);
            dv = db.ExecuteDataView("usp_ArtistWork_ListByArtistID", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListFirstApprovedWorkItem(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_ArtistWork_ListFirstApprovedWorkItem", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByArtistIDForViewing(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_ArtistWork_ListByArtistIDPublic", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByArtistIDForPreview(int ArtistID, int IsDeleted)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@IsDeleted", IsDeleted);
            dv = db.ExecuteDataView("usp_ArtistWork_ListByArtistIDForPreview", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByID(int ArtistWorkID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistWorkID", ArtistWorkID);
            dv = db.ExecuteDataView("usp_ArtistWork_ListByID", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByIDForEmail(int ArtistWorkID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistWorkID", ArtistWorkID);
            dv = db.ExecuteDataView("usp_ArtistWork_ListByIDForEmail", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static int Add(string WorkName, string Medium, string Price, int Width, int Height, int ArtistID, string UniqueID, string Note, string LastModifiedUser, int exhibitionNo, int presentationTypeId, int workEditionTypeId, int editionNumber, int highestEditionNumber)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int ArtistWorkID = 0;
            db.AddParameter("@WorkName", WorkName);
            db.AddParameter("@Medium", Medium);
            db.AddParameter("@Price", Price);
            db.AddParameter("@Width", Width);
            db.AddParameter("@Height", Height);            
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@UniqueID", UniqueID);
            db.AddParameter("@Note", Note);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            db.AddParameter("@ExhibitionNo", exhibitionNo);

            db.AddParameter("@PresentationTypeId", presentationTypeId);
            db.AddParameter("@WorkEditionTypeId", workEditionTypeId);
            db.AddParameter("@EditionNumber", editionNumber);
            db.AddParameter("@HighestEditionNumber", highestEditionNumber);
            ArtistWorkID = Convert.ToInt32(db.ExecuteScalar("usp_ArtistWork_Add", System.Data.CommandType.StoredProcedure));
            return ArtistWorkID;
        }

        public static void Edit(int ArtistWorkID, string WorkName, string Medium, string Price, decimal Width, decimal Height, string UniqueID, string Note, string LastModifiedUser, int exhibitionNo, int presentationTypeId, int workEditionTypeId, int editionNumber, int highestEditionNumber)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int i = 0;
            db.AddParameter("@ArtistWorkID", ArtistWorkID);
            db.AddParameter("@WorkName", WorkName);
            db.AddParameter("@Medium", Medium);
            db.AddParameter("@Price", Price);
            db.AddParameter("@Width", Width);
            db.AddParameter("@Height", Height);
            db.AddParameter("@UniqueID", UniqueID);
            db.AddParameter("@Note", Note);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            db.AddParameter("@ExhibitionNo", exhibitionNo);

            db.AddParameter("@PresentationTypeId", presentationTypeId);
            db.AddParameter("@WorkEditionTypeId", workEditionTypeId);
            db.AddParameter("@EditionNumber", editionNumber);
            db.AddParameter("@HighestEditionNumber", highestEditionNumber);
            i = db.ExecuteNonQuery("usp_ArtistWork_Edit", System.Data.CommandType.StoredProcedure);
        }

        public static string Delete(int ArtistWorkID, string LastModifiedUser)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = string.Empty;
            db.AddParameter("@ArtistWorkID", ArtistWorkID);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            result =Convert.ToString(db.ExecuteScalar("usp_ArtistWork_Delete", System.Data.CommandType.StoredProcedure));
            return result;
        }

        public static DataView Search(int ArtistID, int IsDeleted, string WorkName, string Note)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@IsDeleted", IsDeleted);
            db.AddParameter("@WorkName", WorkName);
            db.AddParameter("@Note", Note);
            dv = db.ExecuteDataView("usp_ArtistWork_Search", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static string UpdateArtistWorkExhibitionNo(int ArtistWorkID, int LastModifiedUser)
        {

            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = string.Empty;
            db.AddParameter("@ArtistWorkID", ArtistWorkID);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            result = Convert.ToString(db.ExecuteScalar("usp_UpdateArtistWorkExhibitionNo", System.Data.CommandType.StoredProcedure));
            return result;
        }
        public static DataView PresetntationTypes()
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_PresentationType_List", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView WorkEditionTypes()
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_WorkEditionType_List", System.Data.CommandType.StoredProcedure);
            return dv;
        }
    }
}

