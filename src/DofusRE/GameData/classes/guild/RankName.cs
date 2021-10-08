

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class RankName : GameDataClass
    {
        public const String MODULE = "RankNames";
        public int id;
        public uint nameId;
        public int order;
    }
}