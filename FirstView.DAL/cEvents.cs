using System.Data;
using FirstView.Model;
namespace FirstView.BusinessLayer
{
    public class cEvents
    {
        public int AddUpdateEvent(EventModel eventModel)
        {
            return FirstView.DataAccessLayer.cEvents.AddUpdateEvent(eventModel);
        }

        public string Delete(EventModel eventModel)
        {
            return FirstView.DataAccessLayer.cEvents.Delete(eventModel);
        }

        public DataView Search(EventModel eventModel)
        {
            return FirstView.DataAccessLayer.cEvents.Search(eventModel);
        }
        public DataView ArtistGroupList()
        {
            return FirstView.DataAccessLayer.cEvents.ArtistGroupList();
        }
        public DataView CurrentExhibition()
        {
            return FirstView.DataAccessLayer.cEvents.CurrentExhibition();
        }
      
    }
}

