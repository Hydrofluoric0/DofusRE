

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Smiley : AbstractGameDataClass
    {
        public const String MODULE = "Smileys";
        public uint id;
        public uint order;
        public String gfxId;
        public Boolean forPlayers;
        public List<String> triggers;
        public uint referenceId;
        public uint categoryId;
    }
}