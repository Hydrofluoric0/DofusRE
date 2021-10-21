using DofusRE.I18n;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace DofusRE.Tests
{
    public class I18nTests
    {
        [Fact]
        public void I18n_Tests()
        {
            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");
            File.Create(output).Close();

            // reference reader
            var refReader = new I18nReader(@"./assets/i18n_fr.d2i");
            refReader.Read();
            refReader.Dispose();

            var writer = new I18nWriter(output);
            writer.Write(refReader.IndexedTexts.Values.ToList(), refReader.NamedTexts.Values.ToList());
            writer.Dispose();

            var testReader = new I18nReader(output);
            testReader.Read();
            testReader.Dispose();

            Assert.True(refReader.IndexedTexts.Count == testReader.IndexedTexts.Count);
            Assert.True(refReader.NamedTexts.Count == testReader.NamedTexts.Count);

            var refList = refReader.IndexedTexts.ToDictionary(x => x.Key);
            var testList = testReader.IndexedTexts.ToDictionary(x => x.Key);
            foreach (var entry in refReader.IndexedTexts)
            {
                var refText = entry.Value;
                var testText = testList[refText.Key].Value;

                Assert.True(refText == testText);
            }
        }
    }
}
