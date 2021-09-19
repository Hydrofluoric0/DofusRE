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
        public long Length => _reader.Length;
        public Dictionary<int, string> Texts => _texts;

        public D2iFile(string filepath)
        {
            this._texts = new Dictionary<int, string>();
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

            for (uint i = 0; i < indexesLength; i += 9)
            {
                var key = this._reader.ReadInt();
                var diacriticalText = this._reader.ReadBoolean();
                var pointer = this._reader.ReadInt();

                this._indexes[key] = pointer;

                if (diacriticalText)
                {
                    i += 4;
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
            Console.WriteLine($"Named texts indexes table");
            Console.WriteLine($"\tstart position : {this._reader.Position}");
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
            Console.WriteLine($"\tend position : {this._reader.Position}");
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
            var min = -1;
            var max = -1;
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
