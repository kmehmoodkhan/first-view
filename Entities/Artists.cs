using System;
namespace FirstView.Entities
{
    public class Artists
    {
        public int ArtistID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int ArtistTypeID { get; set; }
        public string CV { get; set; }
        public string OriginalFileName { get; set; }
        public string ImageFileName { get; set; }
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
