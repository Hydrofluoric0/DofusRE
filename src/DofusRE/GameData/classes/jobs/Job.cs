

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Job : GameDataClass
    {
        public const String MODULE = "Jobs";
        public int id;
        public uint nameId;
        public int iconId;
    }
}