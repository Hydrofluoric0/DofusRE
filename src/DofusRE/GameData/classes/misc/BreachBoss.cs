

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class BreachBoss : GameDataClass
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