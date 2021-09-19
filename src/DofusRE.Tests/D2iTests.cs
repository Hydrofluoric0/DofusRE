using System;
using System.IO;
using DofusRE.d2i;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DofusRE.Tests
{
    [TestClass]
    class D2iTests
    {
        [TestMethod]
        public void D2i_Tests()
        {
            var d2iReader = new D2iReader(@"./assets/i18n_fr.d2i");
            d2iReader.Read();

            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");
            var d2iWriter = new D2iWriter(output);
            d2iWriter.Write(d2iReader.Texts, d2iReader.NamedTexts);
        }
    }
}
