using DofusRE.GameData;
using DofusRE.IO;
using System;
using System.Collections.Generic;

namespace DofusRE.GameData.classes
{
    public class TypeAction : GameDataClass
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