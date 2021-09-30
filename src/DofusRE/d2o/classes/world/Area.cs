

// Generated on 09/25/2021 21:18:16
using System;
using System.Collections.Generic;
using DofusRE.d2o;
using DofusRE.IO.d2o.classes.custom;

namespace DofusRE.d2o.classes
{
    
    public class Area : AbstractGameDataClass
    {
        public const String MODULE = "Areas";
        public int id;
        public uint nameId;
        public int superAreaId;
        public Boolean containHouses;
        public Boolean containPaddocks;
        public Rectangle bounds;
        public uint worldmapId;
        public Boolean hasWorldMap;
    }
}