

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Companion : GameDataClass
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