

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Companion : AbstractGameDataClass
    {
        public const String MODULE = "Companions";
        public int id;
        public uint nameId;
        public String look;
        public Boolean webDisplay;
        public uint descriptionId;
        public uint startingSpellLevelId;
        public uint assetId;
        public List<uint> characteristics;
        public List<uint> spells;
        public int creatureBoneId;
    }
}