

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class LegendaryTreasureHunt : GameDataClass
    {
        public const String MODULE = "LegendaryTreasureHunts";
        public uint id;
        public uint nameId;
        public uint level;
        public uint chestId;
        public uint monsterId;
        public uint mapItemId;
        public double xpRatio;
    }
}