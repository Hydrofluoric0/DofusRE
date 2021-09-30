using DofusRE.d2i;
using DofusRE.io;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2o
{
    public class D2oReader
    {
        private const string D2O_HEADER = "D2O";

        private BigEndianReader m_reader;
        private Dictionary<int, int> m_indexes;
        private Dictionary<int, AbstractGameDataClass> m_gameDataClasses;
        private Dictionary<int, GameDataClassDefinition> m_classesDefinitions;
        private Dictionary<int, GameDataProcessor> m_processors;

        public Dictionary<int, AbstractGameDataClass> Classes => m_gameDataClasses;
        public Dictionary<int, GameDataClassDefinition> ClassesDefinitions => m_classesDefinitions;

        public D2oReader(string filepath)
        {
            this.m_reader = new BigEndianReader(filepath);
            this.m_indexes = new Dictionary<int, int>();
            this.m_gameDataClasses = new Dictionary<int, AbstractGameDataClass>();
            this.m_processors = new Dictionary<int, GameDataProcessor>();
            this.m_classesDefinitions = new Dictionary<int, GameDataClassDefinition>();
        }

        public void Read()
        {
            readHeader();
            readIndexes();
            readClassesDefinitions();
            readProcessors();
            readClasses();
        }

        private void readHeader()
        {
            var headerBytes = this.m_reader.ReadBytes(D2O_HEADER.Length);
            var header = Encoding.ASCII.GetString(headerBytes);
            if (header != D2O_HEADER)
            {
                throw new Exception("malformated game data file.");
            }
        }

        private void readIndexes()
        {
            var indexesPointer = this.m_reader.ReadInt();
            this.m_reader.Seek(indexesPointer, SeekOrigin.Begin);
            var indexesLength = this.m_reader.ReadInt();

            for (int i = 0; i < indexesLength; i += sizeof(int) + sizeof(int))
            {
                var key = this.m_reader.ReadInt();
                var pointer = this.m_reader.ReadInt();
                this.m_indexes.Add(key, pointer);
            }
        }

        private void readClassesDefinitions()
        {
            var classesCount = this.m_reader.ReadInt();

            for (int i = 0; i < classesCount; i++)
            {
                var id = this.m_reader.ReadInt();
                var name = this.m_reader.ReadUTF();
                var package = this.m_reader.ReadUTF();

                var classDef = new GameDataClassDefinition(id, name, package);
                readClassDefinitionFields(classDef);
                this.m_classesDefinitions.Add(id, classDef);
            }
        }

        private void readClassDefinitionFields(GameDataClassDefinition classDef)
        {
            var fieldsCount = this.m_reader.ReadInt();
            for (int j = 0; j < fieldsCount; j++)
            {
                var fieldName = this.m_reader.ReadUTF();
                var fieldType = (GameDataFieldType)this.m_reader.ReadInt();

                classDef.AddField(new GameDataField(fieldName, fieldType, this.m_reader, this.m_classesDefinitions));
            }
        }

        private void readProcessors()
        {
            if (this.m_reader.BytesAvailable <= 0)
            {
                return;
            }

            var processor = new GameDataProcessor(this.m_reader);
            //this.m_processors.Add(processor. pprocessor);
        }

        private void readClasses()
        {
            int min = -1, max = -1;

            foreach (var entry in this.m_indexes)
            {
                if (min == -1 || max == -1)
                {
                    min = entry.Value;
                    max = entry.Value;
                }
                min = min > entry.Value ? entry.Value : min;
                max = max < entry.Value ? entry.Value : max;

                var key = entry.Key;
                var position = entry.Value;
                this.m_reader.Seek(position, SeekOrigin.Begin);

                var classDefinitionIdentifier = this.m_reader.ReadInt();
                var classDef = this.m_classesDefinitions[classDefinitionIdentifier];
                
                var gameDataClass = classDef.Read();
                this.m_gameDataClasses.Add(key, gameDataClass);
            }
        }
    }
}
 