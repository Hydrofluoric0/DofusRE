using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    public class GameDataField
    {
        public const int NULL_IDENTIFIER = -1431655766;

        private string m_name;
        private GameDataFieldType m_type;
        private BigEndianReader m_reader;
        private Func<int, object> m_readFunction;
        private Dictionary<int, GameDataClassDefinition> m_classDefinitions;

        private List<GameDataFieldType> m_innerTypes;
        private List<Func<int, object>> m_innerReadFunctions;

        public string Name => this.m_name;
        public GameDataFieldType Type => this.m_type;

        public GameDataField(string name, GameDataFieldType type, BigEndianReader reader, Dictionary<int, GameDataClassDefinition> classDefinitions)
        {
            this.m_name = name;
            this.m_type = type;
            this.m_reader = reader;
            this.m_classDefinitions = classDefinitions;

            this.m_innerTypes = new List<GameDataFieldType>();
            this.m_innerReadFunctions = new List<Func<int, object>>();

            this.m_readFunction = defineReadFunction(type);
        }

        private Func<int, object> defineReadFunction(GameDataFieldType type)
        {
            switch (type)
            {
                case GameDataFieldType.INT:
                case GameDataFieldType.I18N:
                    return new Func<int, object>(_ => this.m_reader.ReadInt());
                case GameDataFieldType.BOOL:
                    return new Func<int, object>(_ => this.m_reader.ReadBoolean());
                case GameDataFieldType.STRING:
                    return new Func<int, object>(_ => this.m_reader.ReadUTF());
                case GameDataFieldType.NUMBER:
                    return new Func<int, object>(_ => this.m_reader.ReadDouble());
                case GameDataFieldType.UINT:
                    return new Func<int, object>(_ => this.m_reader.ReadUInt());
                case GameDataFieldType.VECTOR:
                    var typeName = this.m_reader.ReadUTF();
                    var innerType = (GameDataFieldType)this.m_reader.ReadInt();
                    this.m_innerTypes.Add(innerType);
                    var innerReadFunction = this.defineReadFunction(innerType);
                    this.m_innerReadFunctions.Insert(0, innerReadFunction);
                    return readVector;
                default:
                    if (type > 0)
                        return new Func<int, object>((_) => readObject());
                    throw new Exception($"unknown GameDataField type: {type}");
            }
        }

        private object readVector(int depth = 0)
        {
            var length = this.m_reader.ReadInt();
            var type = this.m_innerTypes[depth];
            var list = new List<object>();

            for (int i = 0; i < length; i++)
            {
                var element = this.m_innerReadFunctions[depth](depth + 1);
                list.Add(element);
            }
            return list;
        }

        private object readObject()
        {
            var classId = this.m_reader.ReadInt();
            if (classId == NULL_IDENTIFIER)
            {
                return null;
            }
            var classDefinition = this.m_classDefinitions[classId];
            return classDefinition.Read();
        }

        public object Read()
        {
            return this.m_readFunction(0);
        }
    }
}
