

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class MonsterGrade : GameDataClass
    {
        public uint grade;
        public int monsterId;
        public uint level;
        public int vitality;
        public int paDodge;
        public int pmDodge;
        public int wisdom;
        public int earthResistance;
        public int airResistance;
        public int fireResistance;
        public int waterResistance;
        public int neutralResistance;
        public int gradeXp;
        public int lifePoints;
        public int actionPoints;
        public int movementPoints;
        public int damageReflect;
        public uint hiddenLevel;
        public int strength;
        public int intelligence;
        public int chance;
        public int agility;
    }
}