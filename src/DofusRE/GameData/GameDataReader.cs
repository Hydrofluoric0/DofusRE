using DofusRE.I18n;
using DofusRE.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.GameData
{
    public class GameDataReader : IDisposable
    {
        private const string D2O_HEADER = "D2O";

        private BigEndianReader m_reader;
        private Dictionary<int, int> m_indexes;
        private Dictionary<int, GameDataClass> m_gameDataClasses;
        private Dictionary<int, GameDataClassDefinition> m_classDefinitions;
        private Dictionary<int, GameDataProcessor> m_processors;

        public Dictionary<int, GameDataClass> Classes => m_gameDataClasses;
        public Dictionary<int, GameDataClassDefinition> ClassesDefinitions => m_classDefinitions;

        public GameDataReader(string filepath)
        {
            this.m_reader = new BigEndianReader(filepath);
            this.m_indexes = new Dictionary<int, int>();
            this.m_gameDataClasses = new Dictionary<int, GameDataClass>();
            this.m_processors = new Dictionary<int, GameDataProcessor>();
            this.m_classDefinitions = new Dictionary<int, GameDataClassDefinition>();
        }

        public void Read()
        {
            readHeader();
            readIndexes();
            readClassesDefinitions();
            readProcessors();
            readClasses();

            Dispose();
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
                var classDef = new GameDataClassDefinition().Deserialize(this.m_reader);
                this.m_classDefinitions.Add(classDef.Id, classDef);
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
            foreach (var entry in this.m_indexes)
            {
                var key = entry.Key;
                var position = entry.Value;

                this.m_reader.Seek(position, SeekOrigin.Begin);
                var classDefId = this.m_reader.ReadInt();
                var classDef = this.m_classDefinitions[classDefId];
                var _class = GameDataCenter.GetGameDataClassByName(classDef.Name);
                this.m_reader.Seek(position, SeekOrigin.Begin);

                _class.Deserialize(this.m_reader, this.m_classDefinitions);

                this.m_gameDataClasses.Add(key, _class);
            }
        }

        public void Dispose()
        {
            this.m_reader.Dispose();
        }
    }
}
 