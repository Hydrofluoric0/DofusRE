using DofusRE.io;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2i
{
    public class I18nWriter
    {
        private BigEndianWriter m_writer;
        private List<I18nIndexedText> m_texts;
        private List<I18nNamedText> m_namedTexts;
        private Dictionary<int, int> m_textIndexes;
        private Dictionary<string, int> m_namedTextIndexes;
        private Dictionary<int, int> m_undiacriticalTextIndexes;

        public I18nWriter(string path)
        {
            this.m_writer = new BigEndianWriter(path);
            this.m_textIndexes = new Dictionary<int, int>();
            this.m_namedTextIndexes = new Dictionary<string, int>();
            this.m_undiacriticalTextIndexes = new Dictionary<int, int>();
        }
        
        public void Write(List<I18nIndexedText> texts, List<I18nNamedText> named_texts)
        {
            this.m_texts = texts;
            this.m_namedTexts = named_texts;

            // reserve space for text indexes pointer
            this.m_writer.WriteInt(0);

            // write texts (both named & unnamed) first
            writeTexts();
            writeNamedTexts();

            // write text indexes table pointer at reserved space
            var position = (int)this.m_writer.Position;
            this.m_writer.Seek(0, SeekOrigin.Begin);
            this.m_writer.WriteInt(position);
            this.m_writer.Seek(position, SeekOrigin.Begin);

            // finally, writte indexes tables
            writeTextIndexesTable();
            writeNamedTextIndexesTable();
            writeSortedTextIndexesTable();
        }
        private void writeIndexesTableWrapper(Action func)
        {
            var lengthPosition = (int)this.m_writer.Position;

            // reserve space for table length;
            this.m_writer.WriteInt(0);

            // let the writing function do it's stuff
            var startPosition = (int)this.m_writer.Position;
            func();
            var endPosition = (int)this.m_writer.Position;

            // writes table length in reserved space
            var length = endPosition - startPosition;
            this.m_writer.Seek(lengthPosition, SeekOrigin.Begin);
            this.m_writer.WriteInt(length);
            this.m_writer.Seek(endPosition, SeekOrigin.Begin);
        }
        private void writeTextIndexesTable() => writeIndexesTableWrapper(writeTextIndexes);
        private void writeNamedTextIndexesTable() => writeIndexesTableWrapper(writeNamedTextIndexes);
        private void writeSortedTextIndexesTable() => writeIndexesTableWrapper(writeSortedTextIndexes);
        private void writeTextIndexes()
        {
            foreach (var entry in m_texts)
            {
                var pointer = this.m_textIndexes[entry.Key];

                this.m_writer.WriteInt(entry.Key);
                this.m_writer.WriteBoolean(entry.IsDiacritical);
                this.m_writer.WriteInt(pointer);

                if (entry.IsDiacritical)
                {
                    var undiacPointer = this.m_undiacriticalTextIndexes[entry.Key];
                    this.m_writer.WriteInt(undiacPointer);
                }
            }
        }
        private void writeNamedTextIndexes()
        {
            foreach (var entry in m_namedTexts)
            {
                var pointer = this.m_namedTextIndexes[entry.Key];

                this.m_writer.WriteUTF(entry.Key);
                this.m_writer.WriteInt(pointer);
            }
        }
        private void writeSortedTextIndexes()
        {
            foreach (var entry in m_texts)
            {
                this.m_writer.WriteInt(entry.Key);
            }
        }

        private void writeTexts()
        {
            foreach (var entry in m_texts)
            {
                this.m_textIndexes[entry.Key] = (int)this.m_writer.Position;
                this.m_writer.WriteUTF(entry.Text);
                if (entry.IsDiacritical)
                {
                    this.m_undiacriticalTextIndexes[entry.Key] = (int)this.m_writer.Position;
                    this.m_writer.WriteUTF(entry.UndiacriticalText);
                }
            }
        }
        private void writeNamedTexts()
        {
            foreach (var entry in m_namedTexts)
            {
                this.m_namedTextIndexes[entry.Key] = (int)this.m_writer.Position;
                this.m_writer.WriteUTF(entry.Text);
            }
        }

        public void Dispose()
        {
            this.m_writer.Dispose();
        }
    }
}
