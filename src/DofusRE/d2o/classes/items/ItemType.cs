

// Generated on 09/25/2021 21:18:14
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class ItemType : AbstractGameDataClass
    {
        public const String MODULE = "ItemTypes";
        public int id;
        public uint nameId;
        public uint superTypeId;
        public uint categoryId;
        public Boolean isInEncyclopedia;
        public Boolean plural;
        public uint gender;
        public String rawZone;
        public Boolean mimickable;
        public int craftXpRatio;
        public int evolutiveTypeId;
    }
}