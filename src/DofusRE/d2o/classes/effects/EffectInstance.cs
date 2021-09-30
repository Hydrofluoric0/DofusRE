

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class EffectInstance : AbstractGameDataClass
    {
        public const int IS_DISPELLABLE = 1;
        public const int IS_DISPELLABLE_ONLY_BY_DEATH = 2;
        public const int IS_NOT_DISPELLABLE = 3;
        public uint effectUid;
        public uint effectId;
        public int order;
        public int targetId;
        public String targetMask;
        public int duration;
        public int delay;
        public int random;
        public int group;
        public int modificator;
        public Boolean trigger;
        public String triggers;
        public Boolean visibleInTooltip = true;
        public Boolean visibleInBuffUi = true;
        public Boolean visibleInFightLog = true;
        public Boolean forClientOnly = false;
        public int dispellable = 1;
        public Object zoneSize;
        public uint zoneShape;
        public Object zoneMinSize;
        public Object zoneEfficiencyPercent;
        public Object zoneMaxEfficiency;
        public Object zoneStopAtTarget;
        public int effectElement;
        public int spellId;
        public String rawZone;
    }
}