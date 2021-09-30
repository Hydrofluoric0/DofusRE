

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Mount : AbstractGameDataClass
    {
        public const String MODULE = "Mounts";
        public uint id;
        public uint familyId;
        public uint nameId;
        public String look;
        public uint certificateId;
        public List<EffectInstance> effects;
    }
}