using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldResort.Model
{
    public class HotelResponse
    {
        public HotelsResponse hotel { get; set; }
        public List<TypeRoom> typeRoom { get; set; }
    }
}
