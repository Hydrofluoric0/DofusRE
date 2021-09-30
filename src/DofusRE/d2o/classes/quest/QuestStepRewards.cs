

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class QuestStepRewards : AbstractGameDataClass
    {
        public const String MODULE = "QuestStepRewards";
        public uint id;
        public uint stepId;
        public int levelMin;
        public int levelMax;
        public double kamasRatio;
        public double experienceRatio;
        public Boolean kamasScaleWithPlayerLevel;
        public List<List<uint>> itemsReward;
        public List<uint> emotesReward;
        public List<uint> jobsReward;
        public List<uint> spellsReward;
        public List<uint> titlesReward;
    }
}