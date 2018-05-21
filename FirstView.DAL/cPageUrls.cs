using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstView.Model;

namespace FirstView.BusinessLayer
{
    public class cPageUrls
    {
        public static List<Entities.PageUrls> GetPageUrls(PageUrlsModel pageUrlsModel)
        {
            return FirstView.DataAccessLayer.dPageUrls.GetPageUrls(pageUrlsModel);
        }

        public static string SaveUpdatePageUrls(PageUrlsModel pageUrlsModel)
        {
            return FirstView.DataAccessLayer.dPageUrls.SaveUpdatePageUrls(pageUrlsModel);
        }
        public static int DeletePageUrls(PageUrlsModel pageUrlsModel)
        {
            return FirstView.DataAccessLayer.dPageUrls.DeletePageUrls(pageUrlsModel);
        }
    }
}
