using DofusRE.d2o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.IO.d2o.classes.custom
{
    public class TransformData : AbstractGameDataClass
    {
        public double x;
        public double y;
        public double scaleX;
        public double scaleY;
        public int rotation;
        public string originalClip;
        public string overrideClip;

        public TransformData(double x, double y, double scaleX, double scaleY, int rotation, string originalClip, string overrideClip)
        {
            this.x = x;
            this.y = y;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.rotation = rotation;
            this.originalClip = originalClip;
            this.overrideClip = overrideClip;
        }
    }
}
