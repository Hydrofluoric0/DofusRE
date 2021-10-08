

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
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