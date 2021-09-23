using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections.Generic;

namespace DofusRE.d2o
{
    public class GameDataClassDefinition
    {
        private BigEndianReader m_reader;

        private int m_id;
        private string m_name;
        private string m_package;
        private List<GameDataField> m_fields;

        public string Name => m_name;
        public List<GameDataField> Fields => m_fields;

        public GameDataClassDefinition(int id, string name, string package)
        {
            this.m_id = id;
            this.m_name = name;
            this.m_package = package;

            this.m_fields = new List<GameDataField>();
        }

        public void AddField(GameDataField field)
        {
            this.m_fields.Add(field);
        }

        public Dictionary<string, object> Read()
        {
            var result = new Dictionary<string, object>();

            foreach (var field in this.m_fields)
            {
                var strType = Enum.GetName(typeof(GameDataFieldType), field.Type);
                var value = field.Read();
                result.Add(field.Name, value);
            }

            return result;
        }
    }
}
