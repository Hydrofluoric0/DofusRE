using DofusRE.d2o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.IO.d2o.classes.custom
{
    public class Rectangle : AbstractGameDataClass
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public Rectangle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}
