

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Bonus : AbstractGameDataClass
    {
        public const String MODULE = "Bonuses";
        public int id;
        public uint type;
        public int amount;
        public List<int> criterionsIds;
    }
}