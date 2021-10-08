

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class BreedRole : GameDataClass
    {
        public const String MODULE = "BreedRoles";
        public int id;
        public uint nameId;
        public uint descriptionId;
        public int assetId;
        public int color;
    }
}