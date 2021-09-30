using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DofusRE.d2o
{
    public class GameDataClassDefinition
    {
        private BigEndianReader m_reader;

        private int m_id;
        private string m_name;
        private string m_package;
        private Type m_gameDataClassType;
        private List<GameDataField> m_fields;

        public int Id => m_id;
        public string Name => m_name;
        public string Package => m_package;
        public List<GameDataField> Fields => m_fields;
        public Type GameDataClassType => m_gameDataClassType;

        public GameDataClassDefinition(int id, string name, string package)
        {
            this.m_id = id;
            this.m_name = name;
            this.m_package = package;

            this.m_fields = new List<GameDataField>();
            this.m_gameDataClassType = GameDataCenter.GetGameDataClassByName(name);
        }

        public void AddField(GameDataField field)
        {
            this.m_fields.Add(field);
        }

        public AbstractGameDataClass Read()
        {
            var instance = (AbstractGameDataClass)Activator.CreateInstance(this.m_gameDataClassType);

            foreach (var field in this.m_fields)
            {
                field.Read();
            }

            foreach (var fieldInfo in this.m_gameDataClassType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var field = this.m_fields.Find(f => f.Name == fieldInfo.Name);
                dynamic fieldValue;

                if (field == null)
                {
                    if (typeof(IList).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        fieldValue = null;
                    }
                    if (typeof(AbstractGameDataClass).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        fieldValue = null;
                    }
                    else
                    {
                        fieldValue = 0;
                    }
                }
                else
                {
                    fieldValue = field.Value;
                }

                var value = castValue(fieldInfo.FieldType, fieldValue, 0);
                fieldInfo.SetValue(instance, value);
            }

            return instance;
        }

        // from here, it's dark magic
        private dynamic castValue(Type fieldType, dynamic value, int depth=0)
        {
            if (typeof(IList).IsAssignableFrom(fieldType))
            {
                var list = (IList)value;
                var innerType = fieldType.GenericTypeArguments[0];
                Type listType = typeof(List<>).MakeGenericType(innerType);

                if (list.Count == 0)
                {
                    return null;
                }

                var result = (IList)Activator.CreateInstance(listType);
                foreach (var obj in list)
                {
                    result.Add(castValue(innerType, obj, depth + 1));
                }

                return result;
            }
            else if (typeof(AbstractGameDataClass).IsAssignableFrom(fieldType))
            {
                var instance = (AbstractGameDataClass)Activator.CreateInstance(fieldType);
                var _class = (AbstractGameDataClass)value;
                if (_class == null)
                {
                    return null;
                }

                foreach (var field in instance.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    var v = _class.GetType().GetField(field.Name).GetValue(_class);
                    field.SetValue(instance, castValue(field.FieldType, v, 0));
                }
                return instance;
            }
            else
            {
                return Convert.ChangeType(value, fieldType);
            }

            throw new Exception("wtf");
        }
    }
}
