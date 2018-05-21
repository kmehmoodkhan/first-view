using System.Data;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cApprovals
    {
        public static DataView Search(int ArtistID, int ApprovalStatus)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@ApprovalStatus", ApprovalStatus);
            dv = db.ExecuteDataView("usp_Approvals_Search", System.Data.CommandType.StoredProcedure);
            return dv;
        } 
        public static DataView CheckWorkApprovalStatus(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistID", ArtistID);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_Approvals_CheckStatus", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView CheckProfileApprovalStatus(int ArtistID)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistID", ArtistID);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_ProfileApprovals_CheckStatus", System.Data.CommandType.StoredProcedure);
            return dv;
        }      

        public static void UpdateArtistProfileStatus(int ArtistID, int LastModifiedUser, string ApprovalComment, int? ApprovalStatus)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistID", ArtistID);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            db.AddParameter("@ApprovalComment", ApprovalComment);
            db.AddParameter("@ApprovalStatus", ApprovalStatus);
            i = db.ExecuteNonQuery("usp_Artist_StatusUpdate", System.Data.CommandType.StoredProcedure);
        }
        public static void UpdateArtistWorkStatus(string @ArtistWorkIds, int LastModifiedUser, string ApprovalComment,int? ApprovalStatus)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistWorkIds", ArtistWorkIds);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            db.AddParameter("@ApprovalComment", ApprovalComment);
            db.AddParameter("@ApprovalStatus", ApprovalStatus);
            i = db.ExecuteNonQuery("usp_ArtistWork_StatusUpdate", System.Data.CommandType.StoredProcedure);
        }
    }
}

