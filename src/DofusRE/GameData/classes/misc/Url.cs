

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class Url : GameDataClass
    {
        public const String MODULE = "Url";
        public int id;
        public int browserId;
        public String url;
        public String param;
        public String method;
    }
}