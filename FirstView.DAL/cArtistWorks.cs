﻿using System.Data;
namespace FirstView.BusinessLayer
{
    public class cArtistWorks
    {

        public DataView ListByArtistID(int ArtistID, int IsDeleted)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListByArtistID(ArtistID,IsDeleted);
        }
        public DataView ListFirstApprovedWorkItem(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListFirstApprovedWorkItem(ArtistID);
        }
        public DataView ListByArtistIDForViewing(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListByArtistIDForViewing(ArtistID);
        }
        public DataView ListByArtistIDForPreview(int ArtistID, int IsDeleted)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListByArtistIDForPreview(ArtistID,IsDeleted);
        }
        public DataView ListByID(int ArtistWorkID)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListByID(ArtistWorkID);
        }
        public DataView ListByIDForEmail(int ArtistWorkID)
        {
            return FirstView.DataAccessLayer.cArtistWorks.ListByIDForEmail(ArtistWorkID);
        }
        public int Add(string WorkName, string Medium, string wallPrice,string artistPrice, int Width, int Height, int ArtistID, string UniqueID, string Note, string LastModifiedUser,int exhibitionNo, int presentationTypeId, int workEditionTypeId, int editionNumber, int highestEditionNumber,string ApprixmatePrice)
        {
            return FirstView.DataAccessLayer.cArtistWorks.Add(WorkName,Medium, wallPrice,artistPrice, Width,Height,ArtistID,UniqueID,Note,LastModifiedUser, exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber, ApprixmatePrice);
        }
        public void Edit(int ArtistWorkID, string WorkName, string Medium, string wallPrice,string artistPrice, decimal Width, decimal Height, string UniqueID, string Note, string LastModifiedUser,int exhibitionNo,int presentationTypeId,int workEditionTypeId,int editionNumber,int highestEditionNumber,string estimatedPrice,bool isAdmin)
        {
             FirstView.DataAccessLayer.cArtistWorks.Edit(ArtistWorkID,WorkName, Medium, wallPrice,artistPrice, Width, Height, UniqueID, Note, LastModifiedUser, exhibitionNo, presentationTypeId, workEditionTypeId, editionNumber, highestEditionNumber,estimatedPrice,isAdmin);
        }
        public string Delete(int ArtistWorkID, string LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cArtistWorks.Delete(ArtistWorkID,LastModifiedUser);
        }
        public DataSet Search(int ArtistID, int IsDeleted, string WorkName, string Note,string Pattern)
        {
            return FirstView.DataAccessLayer.cArtistWorks.Search(ArtistID,IsDeleted,WorkName,Note, Pattern);
        }
        public string UpdateArtistWorkExhibitionNo(int ArtistWorkID, int LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cArtistWorks.UpdateArtistWorkExhibitionNo(ArtistWorkID, LastModifiedUser);
        }
        public DataView PresetntationTypes()
        {
            return FirstView.DataAccessLayer.cArtistWorks.PresetntationTypes();
        }
        public DataView WorkEditionTypes()
        {
            return FirstView.DataAccessLayer.cArtistWorks.WorkEditionTypes();
        }

        public string GetArtistCommission(int artistId)
        {
            return FirstView.DataAccessLayer.cArtistWorks.GetArtistCommission(artistId);
        }
    }
}

