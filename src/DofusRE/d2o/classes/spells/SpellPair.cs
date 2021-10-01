

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class SpellPair : GameDataClass
    {
        public const String MODULE = "SpellPairs";
        public int id;
        public uint nameId;
        public uint descriptionId;
        public int iconId;
    }
}