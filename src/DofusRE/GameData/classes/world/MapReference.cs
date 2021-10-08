

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class MapReference : GameDataClass
    {
        public const String MODULE = "MapReferences";
        public int id;
        public double mapId;
        public int cellId;
    }
}