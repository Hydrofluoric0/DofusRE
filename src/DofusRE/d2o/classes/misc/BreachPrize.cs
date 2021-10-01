

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
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