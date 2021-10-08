

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class LegendaryPowerCategory : GameDataClass
    {
        public const String MODULE = "LegendaryPowersCategories";
        public int id;
        public String categoryName;
        public Boolean categoryOverridable;
        public List<int> categorySpells;
    }
}