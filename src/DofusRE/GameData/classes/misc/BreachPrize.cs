

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class BreachPrize : GameDataClass
    {
        public const String MODULE = "BreachPrizes";
        public int id;
        public uint nameId;
        public int currency;
        public String tooltipKey;
        public uint descriptionKey;
        public int categoryId;
        public int itemId;
    }
}