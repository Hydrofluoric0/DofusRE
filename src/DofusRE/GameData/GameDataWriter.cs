using DofusRE.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.GameData
{
    public class GameDataWriter : IDisposable
    {
        private const string D2O_HEADER = "D2O";

        private BigEndianWriter m_writer;
        private Dictionary<int, int> m_indexes;
        private Dictionary<int, GameDataClass> m_classes;
        private Dictionary<int, GameDataClassDefinition> m_definitions;

        public GameDataWriter(string output_path)
        {
            this.m_writer = new BigEndianWriter(output_path);
            this.m_indexes = new Dictionary<int, int>();
        }

        public void Write(Dictionary<int, GameDataClassDefinition> definitions, Dictionary<int, GameDataClass> classes)
        {
            this.m_definitions = definitions;
            this.m_classes = classes;

            writeHeader();
            // reserve space for indexes table pointer
            this.m_writer.WriteInt(0);
            writeClasses();
            writeIndexes();
            writeClassesDefinitions();

            Dispose();
        }

        private void writeHeader()
        {
            var header = Encoding.ASCII.GetBytes(D2O_HEADER);
            this.m_writer.WriteBytes(header);
        }

        private void writeClasses()
        {
            foreach (var entry in this.m_classes)
            {
                var key = entry.Key;
                var _class = entry.Value;
                this.m_indexes[key] = (int)this.m_writer.Position;
                _class.Serialize(this.m_writer, this.m_definitions);
            }
        }

        private void writeIndexes()
        {
            var pos = this.m_writer.Position;

            this.m_writer.Seek(3, SeekOrigin.Begin);
            this.m_writer.WriteInt((int)pos);
            this.m_writer.Seek((int)pos, SeekOrigin.Begin);

            var lengthPosition = (int)this.m_writer.Position;
            // reserve space for table length
            this.m_writer.WriteInt(0);

            var beginPosition = (int)this.m_writer.Position;
            foreach (var entry in this.m_indexes)
            {
                this.m_writer.WriteInt(entry.Key);
                this.m_writer.WriteInt(entry.Value);
            }
            var endPosition = (int)this.m_writer.Position;
            var length = endPosition - beginPosition;

            this.m_writer.Seek(lengthPosition, SeekOrigin.Begin);
            this.m_writer.WriteInt(length);
            this.m_writer.Seek(endPosition, SeekOrigin.Begin);
        }

        private void writeClassesDefinitions()
        {
            this.m_writer.WriteInt(this.m_definitions.Count);

            foreach (var entry in m_definitions)
            {
                var classDef = entry.Value;
                classDef.Serialize(this.m_writer);
            }
        }

        public void Dispose()
        {
            this.m_writer.Dispose();
        }
    }
}
