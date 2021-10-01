using DofusRE.d2o;
using DofusRE.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o.classes
{
    public class TransformData : GameDataClass
    {
        public double x;
        public double y;
        public double scaleX;
        public double scaleY;
        public int rotation;
        public string originalClip;
        public string overrideClip;
    }
}
