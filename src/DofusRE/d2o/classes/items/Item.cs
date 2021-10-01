

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Item : GameDataClass
    {
        public const String MODULE = "Items";
        public const int MAX_JOB_LEVEL_GAP = 100;
        public int id;
        public uint nameId;
        public uint typeId;
        public uint descriptionId;
        public int iconId;
        public uint level;
        public uint realWeight;
        public Boolean cursed;
        public int useAnimationId;
        public Boolean usable;
        public Boolean targetable;
        public Boolean exchangeable;
        public double price;
        public Boolean twoHanded;
        public Boolean etheral;
        public int itemSetId;
        public String criteria;
        public String criteriaTarget;
        public Boolean hideEffects;
        public Boolean enhanceable;
        public Boolean nonUsableOnAnother;
        public uint appearanceId;
        public Boolean secretRecipe;
        public List<uint> dropMonsterIds;
        public List<uint> dropTemporisMonsterIds;
        public uint recipeSlots;
        public List<uint> recipeIds;
        public Boolean objectIsDisplayOnWeb;
        public Boolean bonusIsSecret;
        public List<EffectInstance> possibleEffects;
        public List<uint> evolutiveEffectIds;
        public List<uint> favoriteSubAreas;
        public uint favoriteSubAreasBonus;
        public int craftXpRatio;
        public String craftVisible;
        public String craftFeasible;
        public Boolean needUseConfirm;
        public Boolean isDestructible;
        public Boolean isSaleable;
        public List<List<double>> nuggetsBySubarea;
        public List<uint> containerIds;
        public List<List<int>> resourcesBySubarea;
        public ItemType type;
        public uint weight;
    }
}