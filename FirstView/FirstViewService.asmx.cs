using System;
using System.Data;
using System.Globalization;
using System.Web.Services;

using FirstView.BusinessLayer;
using FirstView.Model;

using Newtonsoft.Json;

namespace FirstView
{
    /// <summary>
    /// Summary description for FirstViewService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FirstViewService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string HelloWorld(string name, int age)
        {
            if (Session["FV_UserID"] == null)
            {
                return "Unauthurized.";
            }
            else
            {
                return "Hello";
            }

        }

        [WebMethod(EnableSession = true)]
        public string SearchEventList(EventModel objEvent)
        {
            if (Session["FV_UserID"] != null)
            {
                if (!string.IsNullOrEmpty(objEvent.StartDate))
                {
                    string[] startDate = objEvent.StartDate.Split('/');
                int startYear = Convert.ToInt32(startDate[2]);
                int startMonth = Convert.ToInt32(startDate[1]);
                int startDay = Convert.ToInt32(startDate[0]);
                objEvent.StartDateTime = new DateTime(startYear, startMonth, startDay);
                }
                else
                {
                    objEvent.StartDateTime = new DateTime(1900, 01, 01);
                }
                if (!string.IsNullOrEmpty(objEvent.EndDate))
                {
                    string[] endtDate = objEvent.EndDate.Split('/');
                    int endYear = Convert.ToInt32(endtDate[2]);
                    int endMonth = Convert.ToInt32(endtDate[1]);
                    int endDay = Convert.ToInt32(endtDate[0]);
                    objEvent.EndDateTime = new DateTime(endYear, endMonth, endDay);
                }
                else
                {
                    objEvent.EndDateTime = new DateTime(1900, 01, 01);
                }

                cEvents objcEvents = new cEvents();
                DataView dv = objcEvents.Search(objEvent);
                DataTable dtEvents = dv.ToTable();
                DataTable dt = FormatDataTable(dtEvents);
                string JSONoutput2 = "";
                if (dtEvents != null && dtEvents.Rows.Count > 0)
                {
                    JSONoutput2 = JsonConvert.SerializeObject(dt, Formatting.Indented);
                }
                return JSONoutput2;
            }
            else
            {
                return JsonConvert.SerializeObject("unauthorized.", Formatting.Indented);
            }
        }
        [WebMethod(EnableSession = true)]
        public string PublicAccessEvents()
        {
            cEvents objcEvents = new cEvents();
            EventModel eventModel = new EventModel();
            eventModel.numericStatus = 1;
            eventModel.StartDateTime =DateTime.Now.AddYears(-5);
            eventModel.EndDateTime = DateTime.Now.AddYears(5);
            DataView dv = objcEvents.Search(eventModel);
            DataTable dtEvents = dv.ToTable();
            DataTable dt = FormatDataTable(dtEvents);
            string JSONoutput2 = "";
            if (dtEvents != null && dtEvents.Rows.Count > 0)
            {
                JSONoutput2 = JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            return JSONoutput2;
        }

        [WebMethod(EnableSession = true)]
        public string GetCurrentExhibition()
        {
            cEvents objcEvents = new cEvents();
            DataView dv = objcEvents.CurrentExhibition();
            DataTable dtEvents = dv.ToTable();
            string JSONoutput2 = "";
            if (dtEvents != null && dtEvents.Rows.Count > 0)
            {
                JSONoutput2 = JsonConvert.SerializeObject(dtEvents, Formatting.Indented);
            }
            return JSONoutput2;
        }
        [WebMethod(EnableSession = true)]
        public string UpdateArtistWorkExhibitionNo(int ArtistWorkId)
        {
            if (Session["FV_UserID"] == null)
            {
                return "Unauthurized.";
            }
            else
            {
                cArtistWorks objArtistWorks = new cArtistWorks();
                string result = objArtistWorks.UpdateArtistWorkExhibitionNo(ArtistWorkId, Convert.ToInt32(Session["FV_UserID"]));
                string JSONoutput2 = "";
                JSONoutput2 = JsonConvert.SerializeObject(result, Formatting.Indented);
                return JSONoutput2;
            }
        }
        [WebMethod(EnableSession = true)]
        public string AddArtistToExhibitionNo(int ArtistId)
        {
            if (Session["FV_UserID"] == null)
            {
                return "Unauthurized.";
            }
            else
            {
                cArtist objArtist = new cArtist();
                string result = objArtist.AddArtistToExhibitionNo(ArtistId, Convert.ToInt32(Session["FV_UserID"]));
                string JSONoutput2 = "";
                JSONoutput2 = JsonConvert.SerializeObject(result, Formatting.Indented);
                return JSONoutput2;
            }
        }
        private static DataTable FormatDataTable(DataTable dt)
        {
            DataTable dtFormat = new DataTable();
            dtFormat.Columns.Add("EventId");
            dtFormat.Columns.Add("StartDateText");
            dtFormat.Columns.Add("StartDate");
            dtFormat.Columns.Add("EndDate");
            dtFormat.Columns.Add("EndDateText");
            dtFormat.Columns.Add("StartTimeId");
            dtFormat.Columns.Add("StartTime");
            dtFormat.Columns.Add("EndTimeId");
            dtFormat.Columns.Add("EndTime");
            dtFormat.Columns.Add("Title");
            dtFormat.Columns.Add("ArtistGroup");
            dtFormat.Columns.Add("Summary");
            dtFormat.Columns.Add("EventDetails");
            dtFormat.Columns.Add("NewFileName");
            dtFormat.Columns.Add("OriginalFileName");
            dtFormat.Columns.Add("IsActive");

            dtFormat.Columns.Add("IsExhibition");
            dtFormat.Columns.Add("IsCurrent");
            dtFormat.Columns.Add("ExhibitionNo");

            dtFormat.AcceptChanges();
            DataRow drDtFormat = null;
            foreach (DataRow dr in dt.Rows)
            {
                drDtFormat = dtFormat.NewRow();
                drDtFormat["EventId"] = dr["EventId"];

                drDtFormat["StartTimeId"] = dr["StartTimeId"];
                drDtFormat["StartTime"] = Convert.ToString(dr["StartTime"]) == null ? "" : Convert.ToString(dr["StartTime"]);
                drDtFormat["EndTimeId"] = dr["EndTimeId"];
                drDtFormat["EndTime"] = Convert.ToString(dr["EndTime"]) == null ? "" : Convert.ToString(dr["EndTime"]);
                drDtFormat["Title"] = Convert.ToString(dr["Title"]) == null ? "" : Convert.ToString(dr["Title"]);
                drDtFormat["ArtistGroup"] = Convert.ToString(dr["ArtistGroup"]) == null ? "" : Convert.ToString(dr["ArtistGroup"]);
                drDtFormat["Summary"] = Convert.ToString(dr["Summary"]) == null ? "" : Convert.ToString(dr["Summary"]);
                drDtFormat["EventDetails"] = Convert.ToString(dr["EventDetails"]) == null ? "" : Convert.ToString(dr["EventDetails"]);
                drDtFormat["NewFileName"] = Convert.ToString(dr["NewFileName"]) == null ? "" : Convert.ToString(dr["NewFileName"]);
                drDtFormat["OriginalFileName"] = Convert.ToString(dr["OriginalFileName"]) == null ? "" : Convert.ToString(dr["OriginalFileName"]);
                drDtFormat["IsActive"] = dr["IsActive"];

                drDtFormat["IsExhibition"] = dr["IsExhibition"];
                drDtFormat["IsCurrent"] = dr["IsCurrent"];
                drDtFormat["ExhibitionNo"] = dr["ExhibitionNo"];

                if (!string.IsNullOrEmpty(Convert.ToString(dr["StartDate"])))
                {
                    DateTime dtStart = Convert.ToDateTime(dr["StartDate"]);
                    drDtFormat["StartDateText"] = FormatDate(dtStart);
                    //drDtFormat["StartDate"] = MMDDYYY(Convert.ToDateTime(dr["StartDate"]));
                    drDtFormat["StartDate"] = dtStart.ToString("dd/MM/yyyy");

                }
                if (!string.IsNullOrEmpty(Convert.ToString(dr["EndDate"])))
                {
                    DateTime dtEnd = Convert.ToDateTime(dr["EndDate"]);
                    drDtFormat["EndDateText"] = FormatDate(dtEnd);
                    //drDtFormat["EndDate"] = MMDDYYY(Convert.ToDateTime(dr["EndDate"]));
                    drDtFormat["EndDate"] = dtEnd.ToString("dd/MM/yyyy");
                }
                dtFormat.Rows.Add(drDtFormat);
            }
            return dtFormat;
        }
        private static string MMDDYYY(DateTime dates)
        {
            string days = string.Empty;
            string month = string.Empty;
            string year = dates.Year.ToString();

            if (dates.Day < 10)
            {
                days = "0" + Convert.ToString(dates.Day);
            }
            else
            {
                days = Convert.ToString(dates.Day);
            }
            if (dates.Month < 10)
            {
                month = "0" + Convert.ToString(dates.Month);
            }
            else
            {
                month = Convert.ToString(dates.Month);
            }
            string date = month + "/" + days + "/" + year;
            return date;
        }
        private static string FormatDate(DateTime dates)
        {
            int days = dates.Day;
            int month = dates.Month;
            int year = dates.Year;
            string date = GetDayName(days) + " " + GetDayFormat(days) + " " + MonthName(month);
            return date;
        }
        private static string GetDayFormat(int day)
        {
            string suffix = "th";
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    suffix = "st";
                    break;
                case 2:
                case 22:
                    suffix = "nd";
                    break;
                case 3:
                case 23:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }
            return day.ToString() + suffix;
        }
        private static string GetDayName(int day)
        {
            string DayName = string.Empty;
            switch (day)
            {
                case 1:
                    DayName = "Monday";
                    break;
                case 2:
                    DayName = "Tuesday";
                    break;
                case 3:
                    DayName = "Wednesday";
                    break;
                case 4:
                    DayName = "Thursday";
                    break;
                case 5:
                    DayName = "Friday";
                    break;
                case 6:
                    DayName = "Saturday";
                    break;
                case 7:
                    DayName = "Sunday";
                    break;
            }
            return DayName;
        }
        private static string MonthName(int month)
        {
            string monthName = "th";
            switch (month)
            {
                case 1:
                    monthName = "January";
                    break;
                case 2:
                    monthName = "February";
                    break;
                case 3:
                    monthName = "March";
                    break;
                case 4:
                    monthName = "April";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "June";
                    break;
                case 7:
                    monthName = "July";
                    break;
                case 8:
                    monthName = "August";
                    break;
                case 9:
                    monthName = "September";
                    break;
                case 10:
                    monthName = "October";
                    break;
                case 11:
                    monthName = "November";
                    break;
                case 12:
                    monthName = "December";
                    break;
            }
            return monthName;
        }
        [WebMethod(EnableSession = true)]
        public string DeleteEvent(int Id)
        {
            if (Session["FV_UserID"] != null)
            {
                cEvents objcEvents = new cEvents();
                EventModel eventModel = new EventModel();
                eventModel.EventId = Id;
                eventModel.UserId = Convert.ToInt32(Session["FV_UserID"]);
                string result = objcEvents.Delete(eventModel);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject("unauthorized.", Formatting.Indented);
            }
        }
        [WebMethod(EnableSession = true)]
        public string SaveEvents(EventModel objEvent)
        {
            if (Session["FV_UserID"] != null)
            {
                string JSONoutput2 = "";
                if (objEvent.StartDate != null && objEvent.Title != null)
                {
                    string[] startDate = objEvent.StartDate.Split('/');
                    int startYear = Convert.ToInt32(startDate[2]);
                    int startMonth = Convert.ToInt32(startDate[1]);
                    int startDay = Convert.ToInt32(startDate[0]);
                    objEvent.StartDateTime = new DateTime(startYear, startMonth, startDay);
                    if (!string.IsNullOrEmpty(objEvent.EndDate))
                    {
                        string[] endtDate = objEvent.EndDate.Split('/');
                        int endYear = Convert.ToInt32(endtDate[2]);
                        int endMonth = Convert.ToInt32(endtDate[1]);
                        int endDay = Convert.ToInt32(endtDate[0]);
                        objEvent.EndDateTime = new DateTime(endYear, endMonth, endDay);
                    }
                    else
                    {
                        objEvent.EndDateTime= new DateTime(1900, 01, 01);
                    }
                    
                    cEvents objcEvents = new cEvents();
                    objEvent.Status = true;
                    objEvent.UserId = Convert.ToInt32(Session["FV_UserID"]);
                    int result = objcEvents.AddUpdateEvent(objEvent);
                    if (result > 0)
                    {
                        JSONoutput2 = JsonConvert.SerializeObject("success", Formatting.Indented);
                    }
                    else if (result == -1)
                    {
                        JSONoutput2 = JsonConvert.SerializeObject("-1", Formatting.Indented);
                    }
                    else
                    {
                        JSONoutput2 = JsonConvert.SerializeObject("failed", Formatting.Indented);
                    }
                }
                else
                {
                    JSONoutput2 = JsonConvert.SerializeObject("required.", Formatting.Indented);
                }
                return JSONoutput2;
            }
            else
            {
                return JsonConvert.SerializeObject("unauthorized.", Formatting.Indented);
            }
        }
    }
}
