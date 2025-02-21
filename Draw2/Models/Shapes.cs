using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Draw2.Models
{
    [XmlInclude(typeof(MySquare))]
    [XmlInclude(typeof(MyRectangle))]
    [XmlInclude(typeof(MyPolygon))]
    [XmlInclude(typeof(MyCircle))]
    public class MyShapes
    {
        
        public int Startx { get; set; }
        public int Starty { get; set; }

    }
}
