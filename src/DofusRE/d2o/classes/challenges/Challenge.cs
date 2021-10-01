

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Challenge : GameDataClass
    {
        public const String MODULE = "Challenge";
        public int id;
        public uint nameId;
        public uint descriptionId;
        public Boolean dareAvailable;
        public List<uint> incompatibleChallenges;
    }
}