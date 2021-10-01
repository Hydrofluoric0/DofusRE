

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class SubArea : GameDataClass
    {
        public const String MODULE = "SubAreas";
        public int id;
        public uint nameId;
        public int areaId;
        public List<AmbientSound> ambientSounds;
        public List<List<int>> playlists;
        public List<double> mapIds;
        public Rectangle bounds;
        public List<int> shape;
        public List<uint> customWorldMap;
        public int packId;
        public uint level;
        public Boolean isConquestVillage;
        public Boolean basicAccountAllowed;
        public Boolean displayOnWorldMap;
        public Boolean mountAutoTripAllowed;
        public List<uint> monsters;
        public List<double> entranceMapIds;
        public List<double> exitMapIds;
        public Boolean capturable;
        public List<uint> achievements;
        public List<List<double>> quests;
        public List<List<double>> npcs;
        public int exploreAchievementId;
        public Boolean isDiscovered;
        public List<int> harvestables;
        public int associatedZaapMapId;
    }
}