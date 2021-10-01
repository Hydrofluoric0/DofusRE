

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class MapReference : GameDataClass
    {
        public const String MODULE = "MapReferences";
        public int id;
        public double mapId;
        public int cellId;
    }
}