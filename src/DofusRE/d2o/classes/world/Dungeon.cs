

// Generated on 09/25/2021 21:18:16
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Dungeon : AbstractGameDataClass
    {
        public const String MODULE = "Dungeons";
        public int id;
        public uint nameId;
        public int optimalPlayerLevel;
        public List<double> mapIds;
        public double entranceMapId;
        public double exitMapId;
    }
}