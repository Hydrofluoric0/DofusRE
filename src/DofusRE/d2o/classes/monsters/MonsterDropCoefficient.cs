

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class MonsterDropCoefficient : GameDataClass
    {
        public uint monsterId;
        public uint monsterGrade;
        public double dropCoefficient;
        public String criteria;
    }
}