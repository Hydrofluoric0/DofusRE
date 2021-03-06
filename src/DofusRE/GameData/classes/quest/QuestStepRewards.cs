

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class QuestStepRewards : GameDataClass
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