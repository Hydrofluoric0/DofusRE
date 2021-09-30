

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Skill : AbstractGameDataClass
    {
        public const String MODULE = "Skills";
        public int id;
        public uint nameId;
        public int parentJobId;
        public Boolean isForgemagus;
        public List<int> modifiableItemTypeIds;
        public int gatheredRessourceItem;
        public List<int> craftableItemIds;
        public int interactiveId;
        public int range;
        public Boolean useRangeInClient;
        public String useAnimation;
        public int cursor;
        public int elementActionId;
        public Boolean availableInHouse;
        public uint levelMin;
        public Boolean clientDisplay;
    }
}