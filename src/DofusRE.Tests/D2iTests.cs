﻿using DofusRE.d2i;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace DofusRE.Tests
{
    public class D2iTests
    {
        [Fact]
        public void D2i_Tests()
        {
            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");

            // reference reader
            var refReader = new D2iReader(@"./assets/i18n_fr.d2i");
            refReader.Read();
            refReader.Dispose();

            var writer = new D2iWriter(output, true);
            writer.Write(refReader.Texts.Values.ToList(), refReader.NamedTexts.Values.ToList());
            writer.Dispose();

            var testReader = new D2iReader(output);
            testReader.Read();
            testReader.Dispose();

            Assert.True(refReader.Texts.Count == testReader.Texts.Count);
            Assert.True(refReader.NamedTexts.Count == testReader.NamedTexts.Count);

            var refList = refReader.Texts.ToDictionary(x => x.Key);
            var testList = testReader.Texts.ToDictionary(x => x.Key);
            foreach (var entry in refReader.Texts)
            {
                var refText = entry.Value;
                var testText = testList[refText.Key].Value;

                Assert.True(refText == testText);
            }
        }
    }
}