using System;
namespace FirstView.Entities
{
    public class Events
    {
        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTimeId { get; set; }
        public int EndTimeId { get; set; }
        public string Title { get; set; }
        public string ArtistGroup { get; set; }
        public string Summary { get; set; }
        public string EventDetails { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int LastModifiedUserId { get; set; }
        public string OriginalFileName { get; set; }
        public string NewFileName { get; set; }
    }
}
