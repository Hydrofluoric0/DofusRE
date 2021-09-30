

// Generated on 09/25/2021 21:18:13
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class CensoredWord : AbstractGameDataClass
    {
        public const String MODULE = "CensoredWords";
        public uint id;
        public uint listId;
        public String language;
        public String word;
        public Boolean deepLooking;
    }
}