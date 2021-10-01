

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
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