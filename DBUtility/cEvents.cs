using System;
using System.Data;
using FirstView.Model;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cEvents 
    {
        public static int AddUpdateEvent(EventModel eventModel)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@EventId", eventModel.EventId);
            db.AddParameter("@StartDate", eventModel.StartDateTime);
            db.AddParameter("@EndDate", eventModel.EndDateTime);
            db.AddParameter("@StartTimeId", eventModel.StartTimeId);
            db.AddParameter("@EndTimeId", eventModel.EndTimeId);
            db.AddParameter("@ArtistGroup", eventModel.ArtistGroup);
            db.AddParameter("@Title", eventModel.Title);
            db.AddParameter("@Summary", eventModel.Summary);
            db.AddParameter("@EventDetails", eventModel.EventDetails);
            db.AddParameter("@IsExhibition", eventModel.IsExhibition);
            db.AddParameter("@IsCurrent", eventModel.IsCurrent);
            db.AddParameter("@ExhibitionNo", eventModel.ExhibitionNo);
            db.AddParameter("@UniqueID", eventModel.UniqueID);
            db.AddParameter("@UserId", eventModel.UserId);
            int result = Convert.ToInt32(db.ExecuteScalar("usp_Event_AddUpdate", System.Data.CommandType.StoredProcedure));
            return result;
        }

        public static string Delete(EventModel eventModel)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = string.Empty;
            db.AddParameter("@EventId", eventModel.EventId);
            db.AddParameter("@UserId", eventModel.UserId);
            result = Convert.ToString(db.ExecuteScalar("usp_Event_Delete", System.Data.CommandType.StoredProcedure));
            return result;
        }

        public static DataView Search(EventModel eventModel)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@EventID", eventModel.EventId);
            db.AddParameter("@numericStatus", eventModel.numericStatus);
            db.AddParameter("@StartDate", eventModel.StartDateTime);
            db.AddParameter("@EndDate", eventModel.EndDateTime);
            db.AddParameter("@ArtistGroup", eventModel.ArtistGroup);
            db.AddParameter("@Title", eventModel.Title);
            dv = db.ExecuteDataView("usp_Event_EventList", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView ArtistGroupList()
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();           
            dv = db.ExecuteDataView("usp_Events_GetAllArtistGroups", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        public static DataView CurrentExhibition()
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            dv = db.ExecuteDataView("usp_GetCurrentExhibition", System.Data.CommandType.StoredProcedure);
            return dv;
        }
        
    }
}

