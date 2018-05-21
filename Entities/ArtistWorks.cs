using System;
namespace FirstView.Entities
{
    public class ArtistWorks
    {
        public int ArtistWorkID { get; set; }
        public int Stock { get; set; }
        public int ArtistID { get; set; }
        public string OriginalFileName { get; set; }
        public string WorkName { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public decimal Price { get; set; }
        public int NFS { get; set; }
        public int Sold { get; set; }
        public int wUse { get; set; }
        public int Rep { get; set; }
        public int Framed { get; set; }
        public string Medium { get; set; }
        public string ImageFileName { get; set; }
        public string Note { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedUserId { get; set; }
        public int ApprovalStatus { get; set; }
        public string ApprovalComment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
