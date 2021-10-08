

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class NpcAction : GameDataClass
    {
        public const String MODULE = "NpcActions";
        public int id;
        public int realId;
        public uint nameId;
    }
}