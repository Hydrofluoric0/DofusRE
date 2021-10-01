

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class MapPosition : GameDataClass
    {
        public const String MODULE = "MapPositions";
        public double id;
        public int posX;
        public int posY;
        public Boolean outdoor;
        public int capabilities;
        public int nameId;
        public Boolean showNameOnFingerpost;
        public List<AmbientSound> sounds;
        public List<List<int>> playlists;
        public int subAreaId;
        public int worldMap;
        public Boolean hasPriorityOnWorldmap;
        public Boolean allowPrism;
        public Boolean isTransition;
        public uint tacticalModeTemplateId;
    }
}