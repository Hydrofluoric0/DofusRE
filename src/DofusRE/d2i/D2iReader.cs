using DofusRE.io;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DofusRE.d2i
{
    public class D2iReader : IDisposable
    {
        private BigEndianReader _reader;
        private Dictionary<int, int> _indexes;
        private Dictionary<int, int> _textSortIndex;
        private Dictionary<string, int> _namedTextIndexes;
        private Dictionary<int, int> _unDiacriticalIndex;

        private List<D2iText> _texts;
        private List<D2iNamedText> _namedTexts;

        public int Count => _texts.Count;
        public long Length => _reader.Length;
        public List<D2iText> Texts => _texts;
        public List<D2iNamedText> NamedTexts => _namedTexts;

        public D2iReader(string filepath)
        {
            this._texts = new List<D2iText>();
            this._namedTexts = new List<D2iNamedText>();

            this._indexes = new Dictionary<int, int>();
            this._namedTextIndexes = new Dictionary<string, int>();
            this._textSortIndex = new Dictionary<int, int>();
            this._unDiacriticalIndex = new Dictionary<int, int>();

            this._reader = new BigEndianReader(filepath);
        }

        public void Read()
        {
            readTextIndexes();
            readNamedTextIndexes();
            readSortedTextIndexes();
            readTexts();
            readNamedTexts();
        }

        private void readTextIndexes()
        {
            Console.WriteLine($"[readTextIndexes] text indexes pointer read at {_reader.Position}");
            var indexesPointer = this._reader.ReadInt();
            Console.WriteLine($"[readTextIndexes] text indexes pointer value : {indexesPointer}");
            this._reader.Seek(indexesPointer, SeekOrigin.Begin);
            Console.WriteLine($"[readTextIndexes] text indexes length read at {_reader.Position}");
            var indexesLength = this._reader.ReadInt();
            Console.WriteLine($"[readTextIndexes] text indexes length value : {indexesLength}");

            Console.WriteLine($"[readTextIndexes] text indexes start position : {this._reader.Position}");
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
            }
            Console.WriteLine($"[readTextIndexes] text indexes end position : {this._reader.Position}");
        }

        private void readNamedTextIndexes()
        {
            var indexesLength = this._reader.ReadInt();
            do
            {
                var position = this._reader.Position;
                var key = this._reader.ReadUTF();
                var pointer = this._reader.ReadInt();

                this._namedTextIndexes[key] = pointer;

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
                var key = entry.Key;
                var pointer = entry.Value;

                _reader.Seek(pointer, SeekOrigin.Begin);
                var text = _reader.ReadUTF();

                var isDiac = this._unDiacriticalIndex.ContainsKey(key);
                if (isDiac)
                {
                    var undiacPointer = this._unDiacriticalIndex[key];
                    _reader.Seek(undiacPointer, SeekOrigin.Begin);
                    var undiacText = _reader.ReadUTF();

                    this._texts.Add(new D2iText(key, text, isDiac, undiacText));
                }
                else
                {
                    this._texts.Add(new D2iText(key, text, isDiac));
                }
            }
        }

        private void readNamedTexts()
        {
            foreach (var entry in _namedTextIndexes)
            {
                var key = entry.Key;
                var pointer = entry.Value;

                this._reader.Seek(pointer, SeekOrigin.Begin);
                var text = this._reader.ReadUTF();

                this._namedTexts.Add(new D2iNamedText(key, text));
            }
        }

        public void Dispose()
        {
            this._reader.Dispose();
        }
    }
}
