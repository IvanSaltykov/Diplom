using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldResort.Model
{
    public class Country
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public Guid partWorldId { get; set; }
    }
}
