

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Characteristic : GameDataClass
    {
        public const String MODULE = "Characteristics";
        public int id;
        public String keyword;
        public uint nameId;
        public String asset;
        public int categoryId;
        public Boolean visible;
        public int order;
        public Boolean upgradable;
    }
}