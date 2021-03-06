

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class SpellLevel : GameDataClass
    {
        public const String MODULE = "SpellLevels";
        public uint id;
        public uint spellId;
        public uint grade;
        public uint spellBreed;
        public uint apCost;
        public uint minRange;
        public uint range;
        public Boolean castInLine;
        public Boolean castInDiagonal;
        public Boolean castTestLos;
        public uint criticalHitProbability;
        public Boolean needFreeCell;
        public Boolean needTakenCell;
        public Boolean needFreeTrapCell;
        public Boolean rangeCanBeBoosted;
        public int maxStack;
        public uint maxCastPerTurn;
        public uint maxCastPerTarget;
        public uint minCastInterval;
        public uint initialCooldown;
        public int globalCooldown;
        public uint minPlayerLevel;
        public Boolean hideEffects;
        public Boolean hidden;
        public Boolean playAnimation;
        public List<int> statesRequired;
        public List<int> statesAuthorized;
        public List<int> statesForbidden;
        public List<EffectInstanceDice> effects;
        public List<EffectInstanceDice> criticalEffect;
        public List<String> additionalEffectsZones;
    }
}