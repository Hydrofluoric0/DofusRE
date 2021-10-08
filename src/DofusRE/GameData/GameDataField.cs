using DofusRE.I18n;
using DofusRE.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.GameData
{
    public class GameDataField
    {
        public const int NULL_IDENTIFIER = -1431655766;

        public string Name;
        public GameDataFieldType Type;
        public string TypeName;
        public List<GameDataFieldType> InnerTypes;
        public List<string> InnerTypeNames;
        public byte[] RawBuffer;

        public GameDataField()
        {
            this.InnerTypeNames = new List<string>();
            this.InnerTypes = new List<GameDataFieldType>();
        }

        public GameDataField Deserialize(BigEndianReader reader)
        {
            this.Name = reader.ReadUTF();
            this.Type = (GameDataFieldType)reader.ReadInt();

            if (this.Type == GameDataFieldType.VECTOR)
            {
                GameDataFieldType innerType;
                do
                {
                    this.InnerTypeNames.Add(reader.ReadUTF());
                    innerType = (GameDataFieldType)reader.ReadInt();
                    this.InnerTypes.Add(innerType);
                }
                while (innerType == GameDataFieldType.VECTOR);
            }

            return this;
        }
    }
}
