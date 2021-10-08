

using System;
using System.Collections.Generic;
using DofusRE.IO;
using DofusRE.GameData.classes;

namespace DofusRE.GameData.classes
{
    public class ServerCommunity : GameDataClass
    {
        public const String MODULE = "ServerCommunities";
        public int id;
        public uint nameId;
        public String shortId;
        public List<String> defaultCountries;
        public List<int> supportedLangIds;
        public int namingRulePlayerNameId;
        public int namingRuleGuildNameId;
        public int namingRuleAllianceNameId;
        public int namingRuleAllianceTagId;
        public int namingRulePartyNameId;
        public int namingRuleMountNameId;
        public int namingRuleNameGeneratorId;
        public int namingRuleAdminId;
        public int namingRuleModoId;
    }
}