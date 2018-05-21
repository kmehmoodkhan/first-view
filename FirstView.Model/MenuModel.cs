using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstView.Model
{
    public class MenuModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string MenuTitle { get; set; }
        public string ParentMenu { get; set; }
        public string Url { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsArtist { get; set; }
        public bool IsPublic { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int PageId { get; set; }
    }
}
