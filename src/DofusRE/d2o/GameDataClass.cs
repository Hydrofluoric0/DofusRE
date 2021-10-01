using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using DofusRE.io;
using System.Collections;
using System.IO;

namespace DofusRE.d2o
{
    public abstract class GameDataClass
    {
        public int DefinitionId;
        
        private dynamic readField(BigEndianReader reader, Type type, Dictionary<int, GameDataClassDefinition> classesDefs)
        {
            if (typeof(IList).IsAssignableFrom(type))
            {
                var instance = (IList)Activator.CreateInstance(type);
                var innerType = type.GetGenericArguments().Single();
                var listLength = reader.ReadInt();
                for (int i = 0; i < listLength; i++)
                {
                    instance.Add(readField(reader, innerType, classesDefs));
                }
                return instance;
            }
            else if (type.IsSubclassOf(typeof(GameDataClass)))
            {
                var position = (int)reader.Position;
                var classDefId = reader.ReadInt();
                reader.Seek(position, SeekOrigin.Begin);

                var classDef = classesDefs[classDefId];
                var _class = GameDataCenter.GetGameDataClassByName(classDef.Name);

                return _class.Deserialize(reader, classesDefs);
            }
            else
            {
                if (type == typeof(int))
                    return reader.ReadInt();
                if (type == typeof(bool))
                    return reader.ReadBoolean();
                if (type == typeof(uint))
                    return reader.ReadUInt();
                if (type == typeof(double))
                    return reader.ReadDouble();
                if (type == typeof(string))
                {
                    var str = reader.ReadUTF();
                    if (str == "null")
                        str = null;
                    return str;
                }

                throw new Exception("unknown game data field");
            }
        }

        public GameDataClass Deserialize(BigEndianReader reader, Dictionary<int, GameDataClassDefinition> classesDefs)
        {
            this.DefinitionId = reader.ReadInt();
            var classDef = classesDefs[this.DefinitionId];

            var fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList();
            foreach (var fieldDef in classDef.Fields)
            {
                var field = fields.First(x => x.Name == fieldDef.Name);
                var type = field.FieldType;
                var value = readField(reader, type, classesDefs);

                field.SetValue(this, value);
            }
            return this;
        }

        public virtual void Serialize(BigEndianWriter writer, Dictionary<int, GameDataClassDefinition> definitions)
        {
            var fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList();
            var classDef = definitions[this.DefinitionId];

            writer.WriteInt(this.DefinitionId);
            foreach (var fieldDef in classDef.Fields)
            {
                var field = fields.First(x => x.Name == fieldDef.Name);
                dynamic value = field.GetValue(this);

                writeField(writer, definitions, field.FieldType, value);
            }
        }

        private void writeField(BigEndianWriter writer, Dictionary<int, GameDataClassDefinition> definitions, Type type, dynamic value)
        {
            if (typeof(IList).IsAssignableFrom(type))
            {
                var list = (IList)value;
                var innerType = type.GetGenericArguments().Single();

                writer.WriteInt(list.Count);
                foreach (dynamic element in list)
                {
                    writeField(writer, definitions, innerType, element);
                }
            }
            else if (type.IsSubclassOf(typeof(GameDataClass)))
            {
                var _class = (GameDataClass)value;
                _class.Serialize(writer, definitions);
            }
            else
            {
                if (type == typeof(int))
                    writer.WriteInt((int)value);
                else if (type == typeof(bool))
                    writer.WriteBoolean((bool)value);
                else if (type == typeof(uint))
                    writer.WriteUInt((uint)value);
                else if (type == typeof(double))
                    writer.WriteDouble((double)value);
                else if (type == typeof(string))
                {
                    var str = (string)value;
                    if (str == null)
                        str = "null";
                    writer.WriteUTF(str);
                }
                else
                    throw new Exception("unknown game data field");
            }
        }
    }
}
