

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class SuperArea : GameDataClass
    {
        public const String MODULE = "SuperAreas";
        public int id;
        public uint nameId;
        public uint worldmapId;
        public Boolean hasWorldMap;
    }
}