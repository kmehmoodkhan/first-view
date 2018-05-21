using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstView.Model;

namespace FirstView.Models
{
    public class MenuGlobals
    {
        public static List<MenuModel> PublicMenu
        {
            get;set;
        }
        public static List<MenuModel> AdminMenu
        {
            get; set;
        }
        public static List<MenuModel> ArtistMenu
        {
            get; set;
        }
    }
}