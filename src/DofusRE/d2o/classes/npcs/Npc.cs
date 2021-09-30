

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;
using DofusRE.IO.d2o.classes.custom;

namespace DofusRE.d2o.classes
{
    
    public class Npc : AbstractGameDataClass
    {
        public const String MODULE = "Npcs";
        public int id;
        public uint nameId;
        public List<List<int>> dialogMessages;
        public List<List<int>> dialogReplies;
        public List<uint> actions;
        public uint gender;
        public String look;
        public int tokenShop;
        public List<AnimFunNpcData> animFunList;
        public Boolean fastAnimsFun;
    }
}