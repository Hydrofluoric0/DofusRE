using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DofusRE.d2o
{
    public class GameDataClassDefinition
    {
        public int Id;
        public string Name;
        public string Package;
        public Type GameDataClassType;
        public List<GameDataField> Fields;
        public byte[] RawBuffer;

        public GameDataClassDefinition()
        {
            this.Fields = new List<GameDataField>();
        }

        public GameDataClassDefinition Deserialize(BigEndianReader reader)
        {
            GameDataClassDefinition classDef = new GameDataClassDefinition();
            var beginPosition = (int)reader.Position;

            classDef.Id = reader.ReadInt();
            classDef.Name = reader.ReadUTF();
            classDef.Package = reader.ReadUTF();

            var fieldsCount = reader.ReadInt();
            for (int i = 0; i < fieldsCount; i++)
            {
                var field = new GameDataField().Deserialize(reader);
                classDef.Fields.Add(field);
            }

            var debug = classDef.Fields.ToDictionary(x => x.Name);

            var endPosition = (int)reader.Position;
            classDef.RawBuffer = reader.SubBuffer(beginPosition, endPosition);

            return classDef;
        }
        
        // as we are not supposed to modify the class definition
        // we directly write the raw buffer we got from reading
        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteBytes(this.RawBuffer);
        }
    }
}
