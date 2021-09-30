

// Generated on 09/25/2021 21:18:14
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class EvolutiveItemType : AbstractGameDataClass
    {
        public const String MODULE = "EvolutiveItemTypes";
        public int id;
        public uint maxLevel;
        public double experienceBoost;
        public List<int> experienceByLevel;
    }
}