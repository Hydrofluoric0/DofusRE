

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class EvolutiveEffect : GameDataClass
    {
        public const String MODULE = "EvolutiveEffects";
        public int id;
        public int actionId;
        public int targetId;
        public List<List<double>> progressionPerLevelRange;
    }
}