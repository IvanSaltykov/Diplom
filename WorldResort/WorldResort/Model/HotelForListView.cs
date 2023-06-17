using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldResort.Model
{
    public class HotelForListView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Star { get; set; }
        public Guid CityId { get; set; }
        public BitmapImage image { get; set; }
        public string Description { get; set; }
    }
}
