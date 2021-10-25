using DofusRE.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace DofusRE.I18n
{
    public static class I18nWriter
    {
        public static void Write(string path, List<I18nText> texts)
        {
            using var writer = new BigEndianWriter(path);
            var indexedTexts = texts.FindAll(text => text is I18nIndexedText).Cast<I18nIndexedText>().ToList();
            var namedTexts = texts.FindAll(text => text is I18nNamedText).Cast<I18nNamedText>().ToList();

            // reserve space for text indexes pointer
            writer.WriteInt(0);

            // write texts (both named & unnamed) first and get their indexes
            var (indexedTextsIndexes, undiacriticalIndexes) = writeIndexedTexts(writer, indexedTexts);
            var namedTextsIndexes = writeNamedTexts(writer, namedTexts);

            // write text indexes table pointer at reserved space
            var position = (int)writer.Position;
            writer.Seek(0, SeekOrigin.Begin);
            writer.WriteInt(position);
            writer.Seek(position, SeekOrigin.Begin);

            // finally, write indexes tables
            tableWrapper(writer, () => writeIndexedTextsTable(writer, indexedTextsIndexes, undiacriticalIndexes, indexedTexts));
            tableWrapper(writer, () => writeNamedTextsTable(writer, namedTextsIndexes, namedTexts));
            tableWrapper(writer, () => writeSortedIndexedTextsTable(writer, indexedTexts));
        }
        
        // used to write the indexes table's length
        private static void tableWrapper(BigEndianWriter writer, Action func)
        {
            var lengthPosition = (int)writer.Position;

            // reserve space for table length;
            writer.WriteInt(0);

            // let the writing function do it's stuff
            var startPosition = (int)writer.Position;
            func();
            var endPosition = (int)writer.Position;

            // writes table length in reserved space
            var length = endPosition - startPosition;
            writer.Seek(lengthPosition, SeekOrigin.Begin);
            writer.WriteInt(length);
            writer.Seek(endPosition, SeekOrigin.Begin);
        }
        
        private static Tuple<Dictionary<int, int>, Dictionary<int, int>> writeIndexedTexts(BigEndianWriter writer, List<I18nIndexedText> texts)
        {
            Dictionary<int, int> indexedTextsIndexes = new(texts.Count);
            Dictionary<int, int> undiacriticalIndexes = new(texts.Count);
            
            foreach (var entry in texts)
            {
                indexedTextsIndexes[entry.Key] = (int)writer.Position;
                writer.WriteUTF(entry.Text);
                if (entry.IsDiacritical)
                {
                    undiacriticalIndexes[entry.Key] = (int)writer.Position;
                    writer.WriteUTF(entry.UndiacriticalText);
                }
            }

            return new(indexedTextsIndexes, undiacriticalIndexes);
        }
        private static void writeIndexedTextsTable(BigEndianWriter writer, Dictionary<int, int> indexes, Dictionary<int, int> undiacriticalIndexes, List<I18nIndexedText> texts)
        {
            foreach (var entry in texts)
            {
                var pointer = indexes[entry.Key];

                writer.WriteInt(entry.Key);
                writer.WriteBoolean(entry.IsDiacritical);
                writer.WriteInt(pointer);

                if (entry.IsDiacritical)
                {
                    var undiacPointer = undiacriticalIndexes[entry.Key];
                    writer.WriteInt(undiacPointer);
                }
            }
        }

        private static Dictionary<string, int> writeNamedTexts(BigEndianWriter writer, List<I18nNamedText> texts)
        {
            var indexes = new Dictionary<string, int>(texts.Count);
            
            foreach (var namedText in texts)
            {
                indexes[namedText.Key] = (int)writer.Position;
                writer.WriteUTF(namedText.Text);
            }

            return indexes;
        }
        private static void writeNamedTextsTable(BigEndianWriter writer, Dictionary<string, int> indexes, List<I18nNamedText> texts)
        {
            foreach (var entry in texts)
            {
                var pointer = indexes[entry.Key];

                writer.WriteUTF(entry.Key);
                writer.WriteInt(pointer);
            }
        }
        
        private static void writeSortedIndexedTextsTable(BigEndianWriter writer, List<I18nIndexedText> texts)
        {
            texts.Sort((a, b) => a.Key - b.Key);
            
            foreach (var entry in texts)
                writer.WriteInt(entry.Key);
        }
    }
}
