

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class BonusCriterion : GameDataClass
    {
        public const String MODULE = "BonusesCriterions";
        public int id;
        public uint type;
        public int value;
    }
}