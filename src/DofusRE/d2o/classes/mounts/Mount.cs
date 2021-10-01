

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Mount : GameDataClass
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