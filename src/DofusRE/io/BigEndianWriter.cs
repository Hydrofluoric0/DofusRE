using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.IO
{
    public class BigEndianWriter : IDisposable
    {
        private Stream m_stream;

        public BigEndianWriter(string filepath)
        {
            this.m_stream = initFromFile(filepath);
        }

        public long Position => m_stream.Position;
        public long BytesAvailable => m_stream.Length - m_stream.Position;


        private FileStream initFromFile(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Write);
            return fileStream;
        }


        public void Seek(int offset, SeekOrigin origin) => this.m_stream.Seek(offset, origin);

        public void WriteByte(byte value) => this.m_stream.WriteByte(value);

        public void WriteBytes(byte[] buffer) => this.m_stream.Write(buffer, 0, buffer.Length);

        public void WriteDouble(double value)
        {
            var buffer = new byte[sizeof(double)];
            BinaryPrimitives.WriteDoubleBigEndian(buffer, value);

            this.m_stream.Write(buffer, 0, sizeof(double));
        }

        public void WriteBoolean(bool value) => WriteByte(Convert.ToByte(value));

        public void WriteUShort(ushort value)
        {
            var buffer = new byte[sizeof(ushort)];
            BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
            this.m_stream.Write(buffer, 0, sizeof(ushort));
        }

        public void WriteUTF(string utf)
        {
            var content = Encoding.UTF8.GetBytes(utf);
            var length = (ushort)content.Length;

            WriteUShort(length);
            this.m_stream.Write(content, 0, length);
        }

        public void WriteInt(int value)
        {
            var buffer = new byte[sizeof(int)];
            
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
            this.m_stream.Write(buffer, 0, sizeof(int));
        }

        public void WriteUInt(uint value)
        {
            var buffer = new byte[sizeof(uint)];
            
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
            this.m_stream.Write(buffer, 0, sizeof(uint));
        }

        public void WriteShort(short value)
        {
            var buffer = new byte[sizeof(short)];
            BinaryPrimitives.WriteInt16BigEndian(buffer, value);

            this.m_stream.Write(buffer, 0, sizeof(short));
        }

        public void Dispose()
        {
            this.m_stream.Close();
        }
    }
}

