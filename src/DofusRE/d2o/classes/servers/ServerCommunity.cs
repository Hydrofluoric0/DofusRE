

// Generated on 09/25/2021 21:18:15
using System;
using System.Collections.Generic;
using DofusRE.d2o;

namespace DofusRE.d2o.classes
{
    
    public class ServerCommunity : AbstractGameDataClass
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