using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Draw2.Models
{
    public class MyPolygon :MyShapes
    {
        public PointCollection Points { get; set; }
        public int NoOfSides { get; set; }
        public double Radius { get; set; }
    }
}
