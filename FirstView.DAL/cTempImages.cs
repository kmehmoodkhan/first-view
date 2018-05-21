namespace FirstView.BusinessLayer
{
    public class cTempImages
    {
        public bool CheckImageExists(string UniqueID)
        {
            return FirstView.DataAccessLayer.cTempImages.CheckImageExists(UniqueID);
        }

        public void Add(string OriginalFileName, string NewFileName, string UniqueID)
        {
             FirstView.DataAccessLayer.cTempImages.Add(OriginalFileName,NewFileName,UniqueID);
        }

    }
}

