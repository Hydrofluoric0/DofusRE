using DofusRE.io;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2i
{
    public class D2iWriter
    {
        private BigEndianWriter m_writer;
        private List<D2iText> m_texts;
        private List<D2iNamedText> m_namedTexts;
        private Dictionary<int, int> m_textIndexes;
        private Dictionary<string, int> m_namedTextIndexes;
        private Dictionary<int, int> m_undiacriticalTextIndexes;

        public D2iWriter(string output_path)
        {
            this.m_writer = new BigEndianWriter(output_path);
        }
        
        public void Write(List<D2iText> texts, List<D2iNamedText> named_texts)
        {
            this.m_texts = texts;
            this.m_namedTexts = named_texts;
            this.m_textIndexes = new Dictionary<int, int>();
            this.m_namedTextIndexes = new Dictionary<string, int>();
            this.m_undiacriticalTextIndexes = new Dictionary<int, int>();

            writeTexts();
            writeNamedTexts();
            writeTextIndexes();
            writeNamedTextIndexes();
            writeSortedTextIndexes();
        }

        private void writeTexts()
        {
            // reserve space for indexes table pointer
            this.m_writer.WriteInt(0);

            foreach (var entry in m_texts)
            {
                this.m_textIndexes[entry.Key] = (int)this.m_writer.Position;
                this.m_writer.WriteUTF(entry.Text);
                if (entry.isDiacritical)
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
        private void writeTextIndexes()
        {
            var tablePointer = (int)this.m_writer.Position;
            this.m_writer.Seek(0, SeekOrigin.Begin);
            this.m_writer.WriteInt(tablePointer);

            this.m_writer.Seek(tablePointer, SeekOrigin.Begin);
            // reserve space for table length
            this.m_writer.WriteInt(0);

            foreach (var entry in m_texts)
            {
                var pointer = this.m_textIndexes[entry.Key];

                this.m_writer.WriteInt(entry.Key);
                this.m_writer.WriteInt(pointer);
                this.m_writer.WriteBoolean(entry.isDiacritical);
                if (entry.isDiacritical)
                {
                    var undiacPointer = this.m_undiacriticalTextIndexes[entry.Key];
                    this.m_writer.WriteInt(undiacPointer);
                }
            }

            var currentPosition = (int)this.m_writer.Position;
            var tableLength = currentPosition - tablePointer;
            this.m_writer.Seek(tablePointer, SeekOrigin.Begin);
            this.m_writer.WriteInt(tableLength);
            this.m_writer.Seek(currentPosition, SeekOrigin.Begin);
        }

        private void writeNamedTextIndexes()
        {
            // reserve space for table length
            this.m_writer.WriteInt(0);
            var tablePosition = (int)this.m_writer.Position;

            foreach (var entry in m_namedTexts)
            {
                var pointer = this.m_namedTextIndexes[entry.Key];

                this.m_writer.WriteUTF(entry.Key);
                this.m_writer.WriteInt(pointer);
            }

            var currentPosition = (int)this.m_writer.Position;
            var tableLength = currentPosition - tablePosition;
            this.m_writer.Seek(tablePosition, SeekOrigin.Begin);
            this.m_writer.WriteInt(tableLength);
            this.m_writer.Seek(tablePosition, SeekOrigin.Begin);
        }

        private void writeSortedTextIndexes()
        {
            // reserve space for table length
            this.m_writer.WriteInt(0);
            var tablePosition = (int)this.m_writer.Position;

            foreach (var entry in m_texts)
            {
                this.m_writer.WriteInt(entry.Key);
            }

            var currentPosition = (int)this.m_writer.Position;
            var tableLength = currentPosition - tablePosition;
            this.m_writer.Seek(tablePosition, SeekOrigin.Begin);
            this.m_writer.WriteInt(tableLength);
            this.m_writer.Seek(tablePosition, SeekOrigin.Begin);
        }

        public void Dispose()
        {
            this.m_writer.Dispose();
        }
    }
}
