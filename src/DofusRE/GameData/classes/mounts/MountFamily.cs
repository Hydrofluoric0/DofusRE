

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class MountFamily : GameDataClass
    {
        private const String MODULE = "MountFamily";
        public uint id;
        public uint nameId;
        public String headUri;
    }
}