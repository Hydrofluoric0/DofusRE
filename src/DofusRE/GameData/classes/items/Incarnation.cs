

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Incarnation : GameDataClass
    {
        public const String MODULE = "Incarnation";
        public uint id;
        public String lookMale;
        public String lookFemale;
    }
}