

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;
using DofusRE.IO.d2o.classes.custom;

namespace DofusRE.d2o.classes
{
    
    public class SkinPosition : AbstractGameDataClass
    {
        private const String MODULE = "SkinPositions";
        public uint id;
        public List<TransformData> transformation;
        public List<String> clip;
        public List<uint> skin;
    }
}