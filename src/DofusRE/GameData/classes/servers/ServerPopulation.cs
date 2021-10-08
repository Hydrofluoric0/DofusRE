

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class ServerPopulation : GameDataClass
    {
        public const String MODULE = "ServerPopulations";
        public int id;
        public uint nameId;
        public int weight;
    }
}