

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Hint : GameDataClass
    {
        public const String MODULE = "Hints";
        public int id;
        public uint gfx;
        public uint nameId;
        public double mapId;
        public double realMapId;
        public int x;
        public int y;
        public Boolean outdoor;
        public int subareaId;
        public int worldMapId;
        public uint level;
    }
}