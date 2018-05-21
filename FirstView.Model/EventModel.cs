using System;

namespace FirstView.Model
{
    public  class EventModel
    {
        public int? EventId
        {
            get; set;
        }
        public string StartDate
        {
            get; set;
        }
        public string EndDate
        {
            get; set;
        }
        public DateTime StartDateTime
        {
            get; set;
        }
        public DateTime EndDateTime
        {
            get; set;
        }
        public int? StartTimeId
        {
            get; set;
        }
        public int? EndTimeId
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Summary
        {
            get; set;
        }
        public string ArtistGroup
        {
            get; set;
        }
        public string EventDetails
        {
            get; set;
        }
        public string NewFileName
        {
            get; set;
        }
        public string OriginalFileName
        {
            get; set;
        }
        public Int32 UserId
        {
            get; set;
        }
        public bool? Status
        {
            get;set;
        }
        public int? numericStatus
        {
            get; set;
        }
        public string UniqueID
        {
            get; set;
        }
        public int IsExhibition
        {
            get; set;
        }
        public int IsCurrent
        {
            get; set;
        }
        public int ExhibitionNo
        {
            get; set;
        }
    }
}