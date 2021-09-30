

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Effect : AbstractGameDataClass
    {
        public const String MODULE = "Effects";
        public int id;
        public uint descriptionId;
        public int iconId;
        public int characteristic;
        public uint category;
        public String @operator;
        public Boolean showInTooltip;
        public Boolean useDice;
        public Boolean forceMinMax;
        public Boolean boost;
        public Boolean active;
        public int oppositeId;
        public uint theoreticalDescriptionId;
        public uint theoreticalPattern;
        public Boolean showInSet;
        public int bonusType;
        public Boolean useInFight;
        public uint effectPriority;
        public int elementId;
    }
}