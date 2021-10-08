using DofusRE.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DofusRE.I18n
{
    public class I18nReader : IDisposable
    {
        private BigEndianReader m_reader;
        private Dictionary<int, int> m_textIndexes;
        private Dictionary<int, int> m_sortedTextIndexes;
        private Dictionary<string, int> m_namedTextIndexes;
        private Dictionary<int, int> m_undiacriticalTextIndexes;

        private Dictionary<int, I18nIndexedText> m_texts;
        private Dictionary<string, I18nNamedText> m_namedTexts;

        public Dictionary<int, I18nIndexedText> Texts => m_texts;
        public Dictionary<string, I18nNamedText> NamedTexts => m_namedTexts;

        public I18nReader(string filepath)
        {
            this.m_texts = new Dictionary<int, I18nIndexedText>();
            this.m_namedTexts = new Dictionary<string, I18nNamedText>();

            this.m_textIndexes = new Dictionary<int, int>();
            this.m_namedTextIndexes = new Dictionary<string, int>();
            this.m_sortedTextIndexes = new Dictionary<int, int>();
            this.m_undiacriticalTextIndexes = new Dictionary<int, int>();

            this.m_reader = new BigEndianReader(filepath);
        }

        public void Read()
        {
            readTextIndexes();
            readNamedTextIndexes();
            readSortedTextIndexes();
            readTexts();
            readNamedTexts();

            Dispose();
        }

        private void readTextIndexes()
        {
            var indexesPointer = this.m_reader.ReadInt();
            this.m_reader.Seek(indexesPointer, SeekOrigin.Begin);
            var indexesLength = this.m_reader.ReadInt();

            for (uint i = 0; i < indexesLength; i += 9)
            {
                var key = this.m_reader.ReadInt();
                var diacriticalText = this.m_reader.ReadBoolean();
                var pointer = this.m_reader.ReadInt();

                this.m_textIndexes[key] = pointer;

                if (diacriticalText)
                {
                    i += 4;
                    this.m_undiacriticalTextIndexes[key] = this.m_reader.ReadInt();
                }
            }
        }

        private void readNamedTextIndexes()
        {
            var indexesLength = this.m_reader.ReadInt();
            do
            {
                var position = this.m_reader.Position;
                var key = this.m_reader.ReadUTF();
                var pointer = this.m_reader.ReadInt();

                this.m_namedTextIndexes[key] = pointer;

                var delta = (int)(this.m_reader.Position - position);
                indexesLength -= delta;
            }
            while (indexesLength > 0);
        }

        private void readSortedTextIndexes()
        {
            var index = 1;
            var indexesLength = this.m_reader.ReadInt();
            while (indexesLength > 0)
            {
                var position = this.m_reader.Position;
                var key = this.m_reader.ReadInt();

                this.m_sortedTextIndexes[key] = index++;

                var delta = (int)(this.m_reader.Position - position);
                indexesLength -= delta;
            }
        }

        private void readTexts()
        {
            foreach (var entry in m_textIndexes)
            {
                var key = entry.Key;
                var pointer = entry.Value;

                    m_reader.Seek(pointer, SeekOrigin.Begin);
                var text = m_reader.ReadUTF();

                var isDiac = this.m_undiacriticalTextIndexes.ContainsKey(key);
                if (isDiac)
                {
                    var undiacPointer = this.m_undiacriticalTextIndexes[key];
                    m_reader.Seek(undiacPointer, SeekOrigin.Begin);
                    var undiacText = m_reader.ReadUTF();

                    this.m_texts.Add(key, new I18nIndexedText(key, text, isDiac, undiacText));
                }
                else
                {
                    this.m_texts.Add(key, new I18nIndexedText(key, text, isDiac));
                }
            }
        }

        private void readNamedTexts()
        {
            foreach (var entry in m_namedTextIndexes)
            {
                var key = entry.Key;
                var pointer = entry.Value;

                this.m_reader.Seek(pointer, SeekOrigin.Begin);
                var text = this.m_reader.ReadUTF();

                this.m_namedTexts.Add(key, new I18nNamedText(key, text));
            }
        }

        public void Dispose()
        {
            this.m_reader.Dispose();
        }
    }
}
