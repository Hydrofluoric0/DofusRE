

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class Pet : AbstractGameDataClass
    {
        public const String MODULE = "Pets";
        public int id;
        public List<int> foodItems;
        public List<int> foodTypes;
        public int minDurationBeforeMeal;
        public int maxDurationBeforeMeal;
        public List<EffectInstance> possibleEffects;
    }
}