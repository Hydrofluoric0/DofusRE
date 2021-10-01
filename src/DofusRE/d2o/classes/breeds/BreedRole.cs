

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
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