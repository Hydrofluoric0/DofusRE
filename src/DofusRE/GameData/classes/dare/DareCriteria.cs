

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class DareCriteria : GameDataClass
    {
        public const String MODULE = "DareCriterias";
        public uint id;
        public uint nameId;
        public uint maxOccurence;
        public uint maxParameters;
        public int minParameterBound;
        public int maxParameterBound;
    }
}