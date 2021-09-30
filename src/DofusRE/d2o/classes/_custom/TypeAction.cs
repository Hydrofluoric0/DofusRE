using DofusRE.d2o;
using System;
using System.Collections.Generic;

namespace DofusRE.IO.d2o.classes.custom
{
    public class TypeAction : AbstractGameDataClass
    {
        public static List<TypeAction> TypeActions = new List<TypeAction>();
        public int id;
        public string elementName;
        public int elementId;
        public TypeAction(int id, string elementName, int elementId)
        {
            this.id = id;
            this.elementName = elementName;
            this.elementId = elementId;
        }
    }
}