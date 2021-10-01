

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class SpellState : GameDataClass
    {
        public const String MODULE = "SpellStates";
        public int id;
        public uint nameId;
        public Boolean preventsSpellCast;
        public Boolean preventsFight;
        public Boolean isSilent;
        public Boolean cantDealDamage;
        public Boolean invulnerable;
        public Boolean incurable;
        public Boolean cantBeMoved;
        public Boolean cantBePushed;
        public Boolean cantSwitchPosition;
        public List<int> effectsIds;
        public String icon = "";
        public int iconVisibilityMask;
        public Boolean invulnerableMelee;
        public Boolean invulnerableRange;
        public Boolean cantTackle;
        public Boolean cantBeTackled;
    }
}