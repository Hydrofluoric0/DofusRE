using DofusRE.io;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    public class D2oWriter
    {
        private BigEndianWriter m_writer;
        private Dictionary<int, int> m_indexes;
        private Dictionary<int, AbstractGameDataClass> m_classes;
        private Dictionary<int, GameDataClassDefinition> m_definitions;

        public D2oWriter(string output_path, bool create=false)
        {
            this.m_writer = new BigEndianWriter(output_path, create);
            this.m_indexes = new Dictionary<int, int>();
        }

        public void Write(Dictionary<int, GameDataClassDefinition> definitions, Dictionary<int, AbstractGameDataClass> classes)
        {
            this.m_definitions = definitions;
            this.m_classes = classes;

            writeHeader();

            // reserve space for indexes table pointer
            this.m_writer.WriteInt(0);

            writeClasses();
            writeIndexes();
            writeClassesDefinitions();

            this.m_writer.Dispose();
        }

        private void writeHeader()
        {
            var header = Encoding.ASCII.GetBytes("D2O");
            this.m_writer.WriteBytes(header);
        }

        private void writeClasses()
        {
            foreach (var entry in this.m_classes)
            {
                var key = entry.Key;
                var _class = entry.Value;
                this.m_indexes[key] = (int)this.m_writer.Position;
                writeClass(_class);
            }
        }

        private void writeClass(AbstractGameDataClass _class) {
            var classFields = _class.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var entry = this.m_definitions.First(d => d.Value.Name == _class.GetType().Name);
            var classDef = entry.Value;

            this.m_writer.WriteInt(classDef.Id);
            foreach (var fieldDef in classDef.Fields)
            {
                var field = classFields.First(f => f.Name == fieldDef.Name);
                writeField(_class, field);
            }
        }

        private void writeField(AbstractGameDataClass _class, FieldInfo field)
        {
            dynamic fieldValue = field.GetValue(_class);
            if (fieldValue == null)
            {
                return;
            }
            Type fieldType = fieldValue.GetType();
            writeField(fieldType, fieldValue);
        }

        private void writeField(Type fieldType, dynamic fieldValue)
        {
            if (fieldType == typeof(int))
                this.m_writer.WriteInt(fieldValue);
            else if (fieldType == typeof(string))
                this.m_writer.WriteUTF(fieldValue);
            else if (fieldType == typeof(double))
                this.m_writer.WriteDouble(fieldValue);
            else if (fieldType == typeof(uint))
                this.m_writer.WriteUInt(fieldValue);
            else if (fieldType == typeof(bool))
                this.m_writer.WriteBoolean(fieldValue);
            else if (fieldType == typeof(List<>))
                writeListField(fieldType, (List<object>)fieldValue);
            else if (fieldType == typeof(AbstractGameDataClass))
                writeClass(fieldValue);
        }

        private void writeListField(Type type, List<object> list)
        {
            var innerType = type.GetGenericArguments()[0];
            this.m_writer.WriteInt(list.Count);
            if (typeof(IList).IsAssignableFrom(innerType))
            {
                foreach (List<object> subList in list)
                {
                    writeListField(innerType, subList);
                }
            }
            else
            {
                foreach (var element in list)
                {
                    writeField(innerType, element);
                }
            }
        }

        private void writeIndexes()
        {
            int min = -1, max = -1;

            var pos = this.m_writer.Position;

            this.m_writer.Seek(3, SeekOrigin.Begin);
            this.m_writer.WriteInt((int)pos);
            this.m_writer.Seek((int)pos, SeekOrigin.Begin);

            var lengthPosition = this.m_writer.Position;
            // reserve space for table length
            this.m_writer.WriteInt(0);

            var beginPosition = this.m_writer.Position;
            foreach (var entry in this.m_indexes)
            {
                if (min == -1 || max == -1)
                {
                    min = entry.Value;
                    max = entry.Value;
                }
                min = min > entry.Value ? entry.Value : min;
                max = max < entry.Value ? entry.Value : max;

                this.m_writer.WriteInt(entry.Key);
                this.m_writer.WriteInt(entry.Value);
            }
            var endPosition = this.m_writer.Position;
            var length = (int)(endPosition - beginPosition);

            this.m_writer.Seek((int)lengthPosition, SeekOrigin.Begin);
            this.m_writer.WriteInt(length);
            this.m_writer.Seek((int)endPosition, SeekOrigin.Begin);
        }

        private void writeClassesDefinitions()
        {
            foreach (var entry in m_definitions)
            {
                var classDef = entry.Value;
                this.m_writer.WriteInt(classDef.Id);
                this.m_writer.WriteUTF(classDef.Name);
                this.m_writer.WriteUTF(classDef.Package);
                this.m_writer.WriteInt(classDef.Fields.Count);
                foreach (var field in classDef.Fields)
                {
                    writeClassDefinitionField(field);
                }
            }
        }

        private void writeClassDefinitionField(GameDataField field)
        {
            this.m_writer.WriteUTF(field.Name);
            this.m_writer.WriteInt((int)field.Type);
            if (field.Type == GameDataFieldType.VECTOR)
            {
                for (int i = 0; i < field.InnerTypes.Count; i++)
                {
                    this.m_writer.WriteUTF(field.InnerTypeNames[i]);
                    this.m_writer.WriteInt((int)field.InnerTypes[i]);
                }
            }
        }
    }
}
