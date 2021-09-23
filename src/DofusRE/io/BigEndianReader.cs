using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace DofusRE.io
{
    public class BigEndianReader : IDisposable
    {
        private Stream m_stream;

        public BigEndianReader(Stream stream)
        {
            if (stream is MemoryStream)
            {
                this.m_stream = stream;
            }
            else
            {
                this.m_stream = new MemoryStream();
                stream.CopyTo(this.m_stream);
            }

            this.m_stream.Seek(0, SeekOrigin.Begin);
        }

        public BigEndianReader(string filepath)
        {
            this.m_stream = initFromFile(filepath);
            this.m_stream.Seek(0, SeekOrigin.Begin);
        }


        public long Position => m_stream.Position;
        public long BytesAvailable => m_stream.Length - m_stream.Position;
        public long Length => m_stream.Length;


        private MemoryStream initFromFile(string path)
        {
            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var read = 0;
                var length = fileStream.Length;
                var buffer = new byte[fileStream.Length];

                do
                {
                    var n = fileStream.Read(buffer, read, (int)length);
                    if (n == 0)
                    {
                        break;
                    }

                    read += n;
                }
                while (read < length);

                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }


        public void Seek(int offset, SeekOrigin origin) => this.m_stream.Seek(offset, origin);

        public byte ReadByte() => (byte)this.m_stream.ReadByte();

        public byte[] ReadBytes(int n)
        {
            var buffer = new byte[n];
            this.m_stream.Read(buffer, 0, n);

            return buffer;
        }

        public double ReadDouble()
        {
            var buffer = new byte[sizeof(double)];
            this.m_stream.Read(buffer, 0, sizeof(double));
            return BinaryPrimitives.ReadDoubleBigEndian(buffer);
        }

        public bool ReadBoolean() => ReadByte() != 0;

        public ushort ReadUShort()
        {
            var buffer = new byte[sizeof(ushort)];
            this.m_stream.Read(buffer, 0, sizeof(ushort));

            return BinaryPrimitives.ReadUInt16BigEndian(buffer);
        }

        public string ReadUTF()
        {
            var length = ReadUShort();
            var content = new byte[length];

            this.m_stream.Read(content, 0, length);

            return Encoding.UTF8.GetString(content);
        }

        public int ReadInt()
        {
            var buffer = new byte[sizeof(int)];
            this.m_stream.Read(buffer, 0, sizeof(int));
            return BinaryPrimitives.ReadInt32BigEndian(buffer);
        }

        public uint ReadUInt()
        {
            var buffer = new byte[sizeof(uint)];
            this.m_stream.Read(buffer, 0, sizeof(uint));
            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
        }

        public short ReadShort()
        {
            var buffer = new byte[sizeof(short)];
            this.m_stream.Read(buffer, 0, sizeof(short));
            return BinaryPrimitives.ReadInt16BigEndian(buffer);
        }

        public void Dispose()
        {
            this.m_stream.Dispose();
        }
    }
}
