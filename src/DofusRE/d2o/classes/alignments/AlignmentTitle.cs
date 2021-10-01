

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class AlignmentTitle : GameDataClass
    {
        public const String MODULE = "AlignmentTitles";
        public int sideId;
        public List<int> namesId;
        public List<int> shortsId;
    }
}