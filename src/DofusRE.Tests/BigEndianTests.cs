using DofusRE.d2i;
using DofusRE.io;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DofusRE.Tests
{
    [TestClass]
    public class BigEndianTests
    {
        public readonly string TmpDirectory = Path.GetTempPath();

        [TestMethod]
        public void BigEndian_Tests()
        {
            const string MAGIC = "ANKAMA";

            var filepath = Path.Combine(TmpDirectory, "2.out");
            var writer = new BigEndianWriter(filepath, true);

            writer.WriteBoolean(true);
            writer.WriteByte(Byte.MaxValue);
            writer.WriteDouble(Double.MaxValue);
            writer.WriteInt(Int32.MinValue);
            writer.WriteUInt(UInt32.MaxValue);
            writer.WriteShort(Int16.MinValue);
            writer.WriteUShort(UInt16.MaxValue);
            writer.WriteUTF(MAGIC);

            writer.Dispose();

            var reader = new BigEndianReader(filepath);
            Assert.IsTrue(reader.ReadBoolean() == true);
            Assert.IsTrue(reader.ReadByte() == Byte.MaxValue);
            Assert.IsTrue(reader.ReadDouble() == Double.MaxValue);
            Assert.IsTrue(reader.ReadInt() == Int32.MinValue);
            Assert.IsTrue(reader.ReadUInt() == UInt32.MaxValue);
            Assert.IsTrue(reader.ReadShort() == Int16.MinValue);
            Assert.IsTrue(reader.ReadUShort() == UInt16.MaxValue);
            Assert.IsTrue(reader.ReadUTF() == MAGIC);
        }

        [TestMethod]
        public void D2i_Tests()
        {
            var d2iReader = new D2iReader(@"./assets/i18n_fr.d2i");
            d2iReader.Read();

            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");
            var d2iWriter = new D2iWriter(output, true);
            d2iWriter.Write(d2iReader.Texts, d2iReader.NamedTexts);
            d2iWriter.Dispose();

            var d2iReaderbis = new D2iReader(output);
            d2iReader.Read();
        }
    }
}
