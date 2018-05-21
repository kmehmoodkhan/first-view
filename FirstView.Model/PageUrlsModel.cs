using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstView.Model
{
    public class PageUrlsModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
