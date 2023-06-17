using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldResort.Model
{
    public class HotelsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public int Star { get; set; }
        public byte[] img { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
