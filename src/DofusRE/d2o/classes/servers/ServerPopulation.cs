

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class ServerPopulation : GameDataClass
    {
        public const String MODULE = "ServerPopulations";
        public int id;
        public uint nameId;
        public int weight;
    }
}