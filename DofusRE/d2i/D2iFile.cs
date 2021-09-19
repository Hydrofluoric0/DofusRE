using DofusRE.io;
using System;
using System.Collections.Generic;
using System.IO;

namespace DofusRE.d2i
{
    public class D2iFile : IDisposable
    {
        private BigEndianReader _reader;
        private Dictionary<int, int> _indexes;
        private Dictionary<int, int> _textSortIndex;
        private Dictionary<string, int> _textIndexes;
        private Dictionary<int, int> _unDiacriticalIndex;
        private Dictionary<int, string> _texts;

        public int Count => _texts.Count;
        public Dictionary<int, string> Texts => _texts;

        public D2iFile(string filepath)
        {
            this._indexes = new Dictionary<int, int>();
            this._textIndexes = new Dictionary<string, int>();
            this._textSortIndex = new Dictionary<int, int>();
            this._unDiacriticalIndex = new Dictionary<int, int>();

            this._reader = new BigEndianReader(filepath);

            readTextIndexes();
            readNamedTextIndexes();
            readSortedTextIndexes();
            readTexts();
        }

        private void readTextIndexes()
        {
            var indexesPointer = this._reader.ReadInt();
            this._reader.Seek(indexesPointer, SeekOrigin.Begin);
            var indexesLength = this._reader.ReadInt();

            // 9 is the length of data read
            for (uint i = 0; i < indexesLength; i += 9)
            {
                var key = this._reader.ReadInt();
                var diacriticalText = this._reader.ReadBoolean();
                var pointer = this._reader.ReadInt();

                this._indexes[key] = pointer;

                if (diacriticalText)
                {
                    i += 4; // as it is a diacritical, we read 4 more bytes
                    this._unDiacriticalIndex[key] = this._reader.ReadInt();
                }
                else
                {
                    this._unDiacriticalIndex[key] = pointer;
                }
            }
        }

        private void readNamedTextIndexes()
        {
            var indexesLength = this._reader.ReadInt();
            do
            {
                var position = this._reader.Position;
                var key = this._reader.ReadUTF();
                var pointer = this._reader.ReadInt();

                this._textIndexes[key] = pointer;

                var delta = (int)(this._reader.Position - position);
                indexesLength -= delta;
            }
            while (indexesLength > 0);
        }

        private void readSortedTextIndexes()
        {
            var index = 1;
            var indexesLength = this._reader.ReadInt();
            while (indexesLength > 0)
            {
                var position = this._reader.Position;
                var key = this._reader.ReadInt();

                this._textSortIndex[key] = index++;

                var delta = (int)(this._reader.Position - position);
                indexesLength -= delta;
            }
        }

        private void readTexts()
        {
            foreach (var entry in _indexes)
            {
                var id = entry.Key;
                var position = entry.Value;

                _reader.Seek(position, SeekOrigin.Begin);
                var text = _reader.ReadUTF();

                _texts[id] = text;
            }
        }

        public string GetText(int key)
        {
            return this._texts[key];
        }

        public void Dispose()
        {
            this._reader.Dispose();
        }
    }
}
