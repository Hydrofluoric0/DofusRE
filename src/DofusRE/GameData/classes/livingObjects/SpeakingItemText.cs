

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class SpeakingItemText : GameDataClass
    {
        public const String MODULE = "SpeakingItemsText";
        public int textId;
        public double textProba;
        public uint textStringId;
        public int textLevel;
        public int textSound;
        public String textRestriction;
    }
}