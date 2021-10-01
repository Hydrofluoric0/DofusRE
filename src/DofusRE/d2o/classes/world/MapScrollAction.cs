

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class MapScrollAction : GameDataClass
    {
        public const String MODULE = "MapScrollActions";
        public double id;
        public Boolean rightExists;
        public Boolean bottomExists;
        public Boolean leftExists;
        public Boolean topExists;
        public double rightMapId;
        public double bottomMapId;
        public double leftMapId;
        public double topMapId;
    }
}