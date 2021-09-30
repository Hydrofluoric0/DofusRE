using DofusRE.d2o;
using System;

namespace DofusRE.IO.d2o.classes.custom
{
    public class AnimFunMonsterData : AbstractGameDataClass
    {
        public string animName;
        public int animWeight;
        public AnimFunMonsterData(String animName, Int32 animWeight)
        {
            this.animName = animName;
            this.animWeight = animWeight;
        }
    }
}
