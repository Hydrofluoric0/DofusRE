using DofusRE.IO;
using System;

namespace DofusRE.GameData
{
    public class GameDataProcessor
    {
        private BigEndianReader m_reader;

        public GameDataProcessor(BigEndianReader reader)
        {
            this.m_reader = reader;

            var length = this.m_reader.ReadInt();
            var offset = this.m_reader.Position + length + 4;
            var read = 0;
            while (read < length)
            {
                var beginPosition = this.m_reader.Position;
                
                var fieldName = this.m_reader.ReadUTF();
                var fieldIndex = this.m_reader.ReadInt() + offset;
                var fieldType = this.m_reader.ReadInt();
                var fieldCount = this.m_reader.ReadInt();

                var endPosition = this.m_reader.Position;
                read += (int)(endPosition - beginPosition);
            }
        }
    }
}