using DofusRE.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DofusRE.I18n
{
    public static class I18nReader
    {
        public static List<I18nText> Read(string path)
        {
            using var reader = new BigEndianReader(path);

            var indexes = readIndexedTextIndexes(reader);
            var indexedTextIndexes = indexes.Item1;
            var undiacriticalTextIndexes = indexes.Item2;
            var namedTextIndexes = readNamedTextIndexes(reader);
            var sortedTextIndexes = readSortedTextIndexes(reader);
            var indexedTexts = readIndexedTexts(reader, indexedTextIndexes, undiacriticalTextIndexes);
            var namedTexts = readNamedTexts(reader, namedTextIndexes);
            
            var texts = indexedTexts.Concat<I18nText>(namedTexts).ToList();
            return texts;
        }

        private static Tuple<Dictionary<int, int>, Dictionary<int, int>> readIndexedTextIndexes(BigEndianReader reader)
        {
            var indexedTextIndexes = new Dictionary<int, int>();
            var undiacriticalTextIndexes = new Dictionary<int, int>();
            
            var indexesPointer = reader.ReadInt();
            reader.Seek(indexesPointer, SeekOrigin.Begin);
            var indexesLength = reader.ReadInt();

            for (uint i = 0; i < indexesLength; i += 9)
            {
                var key = reader.ReadInt();
                var diacriticalText = reader.ReadBoolean();
                var pointer = reader.ReadInt();
                
                indexedTextIndexes[key] = pointer;

                if (diacriticalText)
                {
                    i += 4;
                    undiacriticalTextIndexes[key] = reader.ReadInt();
                }
            }

            return new(indexedTextIndexes, undiacriticalTextIndexes);
        }
        
        private static Dictionary<string, int> readNamedTextIndexes(BigEndianReader reader)
        {
            var namedTextIndexes = new Dictionary<string, int>();
            var indexesLength = reader.ReadInt();

            do
            {
                var position = reader.Position;
                var key = reader.ReadUTF();
                var pointer = reader.ReadInt();

                namedTextIndexes[key] = pointer;

                var delta = (int)(reader.Position - position);
                indexesLength -= delta;
            }
            while (indexesLength > 0);

            return namedTextIndexes;
        }
        
        private static Dictionary<int, int> readSortedTextIndexes(BigEndianReader reader)
        {
            var sortedTextIndexes = new Dictionary<int, int>();
            
            var index = 1;
            var indexesLength = reader.ReadInt();

            while (indexesLength > 0)
            {
                var position = reader.Position;
                var key = reader.ReadInt();

                sortedTextIndexes[key] = index++;

                var delta = (int)(reader.Position - position);
                indexesLength -= delta;
            }

            return sortedTextIndexes;
        }
        
        private static List<I18nIndexedText> readIndexedTexts(BigEndianReader reader, Dictionary<int, int> textIndexes, Dictionary<int, int> undiacriticalTextIndexes)
        {
            var indexedTexts = new List<I18nIndexedText>(textIndexes.Count);
            
            foreach (var entry in textIndexes)
            {
                var key = entry.Key;
                var pointer = entry.Value;

                reader.Seek(pointer, SeekOrigin.Begin);
                var text = reader.ReadUTF();

                var isDiac = undiacriticalTextIndexes.ContainsKey(key);
                if (isDiac)
                {
                    var undiacPointer = undiacriticalTextIndexes[key];
                    reader.Seek(undiacPointer, SeekOrigin.Begin);
                    var undiacText = reader.ReadUTF();

                    indexedTexts.Add(new I18nIndexedText(key, text, isDiac, undiacText));
                }
                else
                {
                    indexedTexts.Add(new I18nIndexedText(key, text, isDiac));
                }
            }

            return indexedTexts;
        }

        private static List<I18nNamedText> readNamedTexts(BigEndianReader reader, Dictionary<string, int> namedTextIndexes)
        {
            List<I18nNamedText> namedTexts = new List<I18nNamedText>(namedTextIndexes.Count);
            
            foreach (var entry in namedTextIndexes)
            {
                var key = entry.Key;
                var pointer = entry.Value;

                reader.Seek(pointer, SeekOrigin.Begin);
                var text = reader.ReadUTF();

                namedTexts.Add(new I18nNamedText(key, text));
            }

            return namedTexts;
        }
    }
}
