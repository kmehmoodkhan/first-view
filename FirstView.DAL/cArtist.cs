﻿using System;
using System.Data;

namespace FirstView.BusinessLayer
{
    public class cArtist
    {
        public DataView List(int IsDeleted)
        {
            return FirstView.DataAccessLayer.cArtist.List(IsDeleted);
        }

        public DataView ListByID(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtist.ListByID(ArtistID);
        }

        public DataView ListByIDForViewing(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtist.ListByIDForViewing(ArtistID);
        }

        public DataView ListByIDForEmail(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtist.ListByIDForEmail(ArtistID);
        }

        public DataView Search(int IsDeleted, string Name, int ArtistTypeID)
        {
            return FirstView.DataAccessLayer.cArtist.Search(IsDeleted,Name,ArtistTypeID);
        }

        public DataView CreateIndex()
        {
            return FirstView.DataAccessLayer.cArtist.CreateIndex();
        }

        public DataView CreateIndexAlphabets()
        {
            return FirstView.DataAccessLayer.cArtist.CreateIndexAlphabets();
        }

        public DataView CreateIndexSearch(string Name, int ArtistTypeID)
        {
            return FirstView.DataAccessLayer.cArtist.CreateIndexSearch(Name,ArtistTypeID);
        }

        public DataView CreateIndexSearchAlpha(string Surname)
        {
            return FirstView.DataAccessLayer.cArtist.CreateIndexSearchAlpha(Surname);
        }

        public DataView CreateIndexAlphabetsSearch(string Name, int ArtistTypeID)
        {
            return FirstView.DataAccessLayer.cArtist.CreateIndexAlphabetsSearch(Name,ArtistTypeID);
        }

        public int Add(string Name, string Surname, string CV, int ArtistTypeID, string UniqueID, string LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cArtist.Add(Name,Surname,CV,ArtistTypeID,UniqueID,LastModifiedUser);
        }

        public void Edit(int ArtistID, string Name, string Surname, string CV, int ArtistTypeID,bool IsDeleted, string UniqueID, string LastModifiedUser)
        {
            FirstView.DataAccessLayer.cArtist.Edit(ArtistID,Name,Surname,CV,ArtistTypeID,IsDeleted,UniqueID,LastModifiedUser);
        }

        public string Delete(int ArtistID, string LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cArtist.Delete(ArtistID, LastModifiedUser);
        }

        public string AddArtistToExhibitionNo(int artistId, int LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cArtist.AddArtistToExhibitionNo(artistId, LastModifiedUser);
        }
        public DataView IsArtitstAllowedInExhibition(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtist.IsArtitstAllowedInExhibition(ArtistID);
        }
        public DataView UserDetailsByArtistId(int ArtistID)
        {
            return FirstView.DataAccessLayer.cArtist.UserDetailsByArtistId(ArtistID);
        }

    }
}
