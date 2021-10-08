

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
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