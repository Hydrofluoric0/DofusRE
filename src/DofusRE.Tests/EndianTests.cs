using DofusRE.d2i;
using DofusRE.io;
using Xunit;
using System;
using System.IO;

namespace DofusRE.Tests
{
    public class BigEndianTests
    {
        public readonly string TmpDirectory = Path.GetTempPath();

        [Fact]
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
            Assert.True(reader.ReadBoolean() == true);
            Assert.True(reader.ReadByte() == Byte.MaxValue);
            Assert.True(reader.ReadDouble() == Double.MaxValue);
            Assert.True(reader.ReadInt() == Int32.MinValue);
            Assert.True(reader.ReadUInt() == UInt32.MaxValue);
            Assert.True(reader.ReadShort() == Int16.MinValue);
            Assert.True(reader.ReadUShort() == UInt16.MaxValue);
            Assert.True(reader.ReadUTF() == MAGIC);
        }
    }
}
