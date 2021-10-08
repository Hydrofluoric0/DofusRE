

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Sign : GameDataClass
    {
        public const String MODULE = "Signs";
        public int id;
        public String @params;
        public int skillId;
        public uint textKey;
    }
}