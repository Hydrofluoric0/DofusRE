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
        // switch to GameDataClass ?
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
            Console.WriteLine($"Deserializing a class definition at {reader.Position}");
            var beginPosition = (int)reader.Position;

            classDef.Id = reader.ReadInt();
            classDef.Name = reader.ReadUTF();
            classDef.Package = reader.ReadUTF();
            Console.WriteLine($"\tid={classDef.Id}");
            Console.WriteLine($"\tName={classDef.Name}");
            Console.WriteLine($"\tPackage={classDef.Package}");

            var fieldsCount = reader.ReadInt();
            Console.WriteLine($"\tFields count={fieldsCount}");
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
