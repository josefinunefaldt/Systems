using Adverts.Data.Entities;
using Subscribers.Data.Entities;

namespace Adverts.ViewModel
{

    public class AdvertViewModel
    {
        public Sub Subscriber { get; set; }
        public Ads Ads { get; set; }
       public List<Ads> AdsList { get; set; } 
      

        public AdvertViewModel()
        {

            AdsList=new List<Ads>();
            Ads = new Ads();
            Subscriber = new Sub();
        }
    }
}

