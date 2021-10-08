

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class AlignmentSide : GameDataClass
    {
        public const String MODULE = "AlignmentSides";
        public int id;
        public uint nameId;
        public Boolean canConquest;
    }
}