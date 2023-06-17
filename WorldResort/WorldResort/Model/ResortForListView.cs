using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldResort.Model
{
    public class ResortForListView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage image { get; set; }
        public bool Climate { get; set; }
        public bool TherapeuticMud { get; set; }
        public bool MineralWater { get; set; }
    }
}
