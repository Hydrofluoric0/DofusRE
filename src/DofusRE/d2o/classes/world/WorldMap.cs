

// Generated on 09/25/2021 21:18:16
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class WorldMap : AbstractGameDataClass
    {
        public const String MODULE = "WorldMaps";
        public int id;
        public uint nameId;
        public int origineX;
        public int origineY;
        public double mapWidth;
        public double mapHeight;
        public Boolean viewableEverywhere;
        public double minScale;
        public double maxScale;
        public double startScale;
        public int totalWidth;
        public int totalHeight;
        public List<String> zoom;
    }
}