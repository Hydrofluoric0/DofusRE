using DofusRE.d2o;

namespace DofusRE.IO.d2o.classes.custom
{
    public class AnimFunNpcData : AbstractGameDataClass
    {
        public string animName;
        public int animWeight;
        public AnimFunNpcData(string animName, int animWeight)
        {
            this.animName = animName;
            this.animWeight = animWeight;
        }
    }
}
