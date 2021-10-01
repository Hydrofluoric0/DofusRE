

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Idol : GameDataClass
    {
        public const String MODULE = "Idols";
        public int id;
        public String description;
        public int categoryId;
        public int itemId;
        public Boolean groupOnly;
        public int spellPairId;
        public int score;
        public int experienceBonus;
        public int dropBonus;
        public List<int> synergyIdolsIds;
        public List<double> synergyIdolsCoeff;
        public List<int> incompatibleMonsters;
    }
}