

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Appearance : GameDataClass
    {
        public const String MODULE = "Appearances";
        public uint id;
        public uint type;
        public String data;
    }
}