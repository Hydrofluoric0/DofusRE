

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class ServerLang : GameDataClass
    {
        public const String MODULE = "ServerLangs";
        public int id;
        public uint nameId;
        public String langCode;
    }
}