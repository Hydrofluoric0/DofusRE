

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
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