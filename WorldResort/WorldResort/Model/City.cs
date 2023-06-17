using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldResort.Model
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string PartWorldId { get; set; }
        public bool Climate { get; set; }
        public bool TherapeuticMud { get; set; }
        public bool MineralWater { get; set; }
        public byte[] img { get; set; }
        public string Description { get; set; }
    }
}
