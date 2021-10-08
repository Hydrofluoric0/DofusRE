

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class CharacterXPMapping : GameDataClass
    {
        public const String MODULE = "CharacterXPMappings";
        public int level;
        public double experiencePoints;
    }
}