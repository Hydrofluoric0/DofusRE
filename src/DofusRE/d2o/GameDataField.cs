using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    public class GameDataField
    {
        public const int NULL_IDENTIFIER = -1431655766;

        private string m_name;
        private dynamic m_value;
        private GameDataFieldType m_type;
        private BigEndianReader m_reader;
        private Func<int, dynamic> m_readFunction;
        private Dictionary<int, GameDataClassDefinition> m_classDefinitions;

        private List<GameDataFieldType> m_innerTypes;
        private List<string> m_innerTypeNames;
        private List<AbstractGameDataClass> m_innerClasses;
        private List<Func<int, dynamic>> m_innerReadFunctions;

        public string Name => this.m_name;
        public dynamic Value => this.m_value;
        public GameDataFieldType Type => this.m_type;
        public List<GameDataFieldType> InnerTypes => this.m_innerTypes;
        public List<string> InnerTypeNames => this.m_innerTypeNames;


        public GameDataField(string name, GameDataFieldType type, BigEndianReader reader, Dictionary<int, GameDataClassDefinition> classDefinitions)
        {
            this.m_name = name;
            this.m_type = type;
            this.m_reader = reader;
            this.m_classDefinitions = classDefinitions;

            this.m_innerTypes = new List<GameDataFieldType>();
            this.m_innerTypeNames = new List<string>();
            this.m_innerClasses = new List<AbstractGameDataClass>();
            this.m_innerReadFunctions = new List<Func<int, dynamic>>();
            this.m_readFunction = defineReadFunction(type);
        }

        private Func<int, dynamic> defineReadFunction(GameDataFieldType type)
        {
            switch (type)
            {
                case GameDataFieldType.INT:
                case GameDataFieldType.I18N:
                    return new Func<int, dynamic>(_ => this.m_reader.ReadInt());
                case GameDataFieldType.BOOL:
                    return new Func<int, dynamic>(_ => this.m_reader.ReadBoolean());
                case GameDataFieldType.STRING:
                    return new Func<int, dynamic>(_ => this.m_reader.ReadUTF());
                case GameDataFieldType.NUMBER:
                    return new Func<int, dynamic>(_ => this.m_reader.ReadDouble());
                case GameDataFieldType.UINT:
                    return new Func<int, dynamic>(_ => this.m_reader.ReadUInt());
                case GameDataFieldType.VECTOR:
                    var innerTypeName = this.m_reader.ReadUTF();
                    this.m_innerTypeNames.Add(innerTypeName);
                    var innerType = (GameDataFieldType)this.m_reader.ReadInt();
                    this.m_innerTypes.Add(innerType);
                    var innerReadFunction = this.defineReadFunction(innerType);
                    this.m_innerReadFunctions.Insert(0, innerReadFunction);
                    return readVector;
                default:
                    if (type > 0)
                        return readObject;
                    throw new Exception($"unknown GameDataField type: {type}");
            }
        }

        private List<dynamic> readVector(int depth)
        {
            var length = this.m_reader.ReadInt();
            if (length == 0)
            {

            }
            var type = this.m_innerTypes[depth];
            var list = new List<dynamic>();

            for (int i = 0; i < length; i++)
            {
                var value = this.m_innerReadFunctions[depth](depth + 1);
                list.Add(value);
            }
            return list;
        }

        private AbstractGameDataClass readObject(int depth)
        {
            var classId = this.m_reader.ReadInt();
            if (classId == NULL_IDENTIFIER)
            {
                return null;
            }

            var classDefinition = this.m_classDefinitions[classId];

            return classDefinition.Read();
        }

        public void Read()
        {
            var value = this.m_readFunction(0);
            this.m_value = value;
        }
    }
}
