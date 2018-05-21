using System;
namespace FirstView.Entities
{
    public class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsArtist { get; set; }
        public int ArtistID { get; set; }
        public string TempPassword { get; set; }
        public DateTime TempPasswordExpire { get; set; }
        public string TempPasswordKey { get; set; }
        public string VerificationCode { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
