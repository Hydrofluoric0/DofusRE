

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class MonsterRace : GameDataClass
    {
        public const String MODULE = "MonsterRaces";
        public int id;
        public int superRaceId;
        public uint nameId;
        public List<uint> monsters;
        public int aggressiveZoneSize;
        public int aggressiveLevelDiff;
        public String aggressiveImmunityCriterion;
        public int aggressiveAttackDelay;
    }
}