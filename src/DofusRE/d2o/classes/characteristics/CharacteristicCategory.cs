

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class CharacteristicCategory : AbstractGameDataClass
    {
        public const String MODULE = "CharacteristicCategories";
        public int id;
        public uint nameId;
        public int order;
        public List<uint> characteristicIds;
    }
}