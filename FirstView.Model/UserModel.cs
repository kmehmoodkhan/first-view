using System;

namespace FirstView.Model
{
    public class UserModel
    {
        public int UserID { get; set; }
        public int ArtistID { get; set; }
        public int ArtistTypeID { get; set; }        
        
        public string Address { get; set; }
        public string Town { get; set; }
        public int Country { get; set; }
        public string ZipCode { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }

        public string BankSortCode { get; set; }
        public string BankAccountNumber { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsArtist { get; set; }
        public bool IsVerified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}