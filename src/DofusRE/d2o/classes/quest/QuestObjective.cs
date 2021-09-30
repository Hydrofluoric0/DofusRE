

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;
using DofusRE.IO.d2o.classes.custom;

namespace DofusRE.d2o.classes
{
    
    public class QuestObjective : AbstractGameDataClass
    {
        public const String MODULE = "QuestObjectives";
        public uint id;
        public uint stepId;
        public uint typeId;
        public int dialogId;
        public QuestObjectiveParameters parameters;
        public Point coords;
        public double mapId;
        public QuestObjectiveType type;
    }
}