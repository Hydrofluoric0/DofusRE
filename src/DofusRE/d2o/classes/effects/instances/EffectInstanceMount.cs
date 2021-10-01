

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class EffectInstanceMount : EffectInstance
    {
        public double id;
        public double expirationDate;
        public uint model;
        public String name = "";
        public String owner = "";
        public uint level = 0;
        public Boolean sex = false;
        public Boolean isRideable = false;
        public Boolean isFeconded = false;
        public Boolean isFecondationReady = false;
        public int reproductionCount = 0;
        public uint reproductionCountMax = 0;
        public List<EffectInstanceInteger> effects;
        public List<uint> capacities;
    }
}