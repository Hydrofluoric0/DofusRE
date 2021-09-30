

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class BreachBoss : AbstractGameDataClass
    {
        public const String MODULE = "BreachBosses";
        public int id;
        public int monsterId;
        public int category;
        public String apparitionCriterion;
        public String accessCriterion;
        public int maxRewardQuantity;
        public List<int> incompatibleBosses;
        public uint rewardId;
    }
}