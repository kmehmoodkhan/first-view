using System.Data;
namespace FirstView.BusinessLayer
{
    public class cApprovals
    {
        public DataView Search(int ArtistID, int ApprovalStatus)
        {
            return FirstView.DataAccessLayer.cApprovals.Search(ArtistID, ApprovalStatus);
        }
        public DataView CheckWorkApprovalStatus(int ArtistID)
        {
            return FirstView.DataAccessLayer.cApprovals.CheckWorkApprovalStatus(ArtistID);
        }
        public DataView CheckProfileApprovalStatus(int ArtistID)
        {
            return FirstView.DataAccessLayer.cApprovals.CheckProfileApprovalStatus(ArtistID);
        }
        public void UpdateArtistProfileStatus(int ArtistID, int LastModifiedUser, string ApprovalComment, int? ApprovalStatus)
        {
            FirstView.DataAccessLayer.cApprovals.UpdateArtistProfileStatus(ArtistID,LastModifiedUser,ApprovalComment, ApprovalStatus);
        }
        public void UpdateArtistWorkStatus(string @ArtistWorkIds, int LastModifiedUser, string ApprovalComment, int? ApprovalStatus)
        {
            FirstView.DataAccessLayer.cApprovals.UpdateArtistWorkStatus(@ArtistWorkIds, LastModifiedUser, ApprovalComment, ApprovalStatus);
        }
    }
}

