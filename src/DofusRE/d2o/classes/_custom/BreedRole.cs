using DofusRE.d2o;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.IO.d2o.classes.custom
{
    public class BreedRole : AbstractGameDataClass
    {
        public static List<BreedRole> BreedRoles = new List<BreedRole>();
        public Int32 id;
        public Int32 nameId;
        public Int32 descriptionId;
        public Int32 assetId;
        public Int32 color;
        public BreedRole(Int32 id, Int32 nameId, Int32 descriptionId, Int32 assetId, Int32 color)
        {
            this.id = id;
            this.nameId = nameId;
            this.descriptionId = descriptionId;
            this.assetId = assetId;
            this.color = color;
        }
    }
}