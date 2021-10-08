

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class ServerGameType : GameDataClass
    {
        public const String MODULE = "ServerGameTypes";
        public int id;
        public Boolean selectableByPlayer;
        public uint nameId;
        public uint rulesId;
        public uint descriptionId;
    }
}