

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class ArenaLeagueReward : AbstractGameDataClass
    {
        public const String MODULE = "ArenaLeagueRewards";
        public int id;
        public uint seasonId;
        public uint leagueId;
        public List<uint> titlesRewards;
        public Boolean endSeasonRewards;
    }
}