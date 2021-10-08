

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class WorldMap : GameDataClass
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