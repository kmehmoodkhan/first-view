namespace FirstView.Entities
{
    public class Addresses
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RefID { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ZipCode { get; set; }
        public int Country { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Mobile { get; set; }
        public string WWW { get; set; }
        public string Email { get; set; }
        public int Type1 { get; set; }
        public int Mail1 { get; set; }
        public decimal Commission { get; set; }
        public int ArtistTypeID { get; set; }
        public int Invite { get; set; }
        public int NotReceived { get; set; }
        public int Dovat { get; set; }
        public decimal Bought { get; set; }
        public int FGA { get; set; }
        public int UpFront { get; set; }
    }
}
