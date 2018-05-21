using System.Data;
namespace FirstView.BusinessLayer
{
    public class cArtistType
    {
        public void Add(string Description)
        {
            FirstView.DataAccessLayer.cArtistType.Add(Description);
        }

        public void Edit(int ArtistTypeID, string Description)
        {
            FirstView.DataAccessLayer.cArtistType.Edit(ArtistTypeID, Description);
        }

        public void Delete(int ArtistTypeID)
        {
            FirstView.DataAccessLayer.cArtistType.Delete(ArtistTypeID);
        }

        public bool CanDelete(int ArtistTypeID)
        {
            return FirstView.DataAccessLayer.cArtistType.CanDelete(ArtistTypeID);
        }

        public bool CheckExistsAdd(string Description)
        {
            return FirstView.DataAccessLayer.cArtistType.CheckExistsAdd(Description);
        }

        public bool CheckExistsEdit(int ArtistTypeID, string Description)
        {
            return FirstView.DataAccessLayer.cArtistType.CheckExistsEdit(ArtistTypeID, Description);
        }

        public DataView ListByID(int ArtistTypeID)
        {
           return FirstView.DataAccessLayer.cArtistType.ListByID(ArtistTypeID);
        }

        public DataView Search(string Description)
        {
            return FirstView.DataAccessLayer.cArtistType.Search(Description);
        }

        public DataView List()
        {
            return FirstView.DataAccessLayer.cArtistType.List();
        }

    }
}

