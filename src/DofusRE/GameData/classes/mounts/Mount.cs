

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
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