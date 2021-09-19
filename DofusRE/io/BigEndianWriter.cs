﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.io
{
    public class BigEndianWriter : IDisposable
    {
        private Stream m_stream;

        public BigEndianWriter(string filepath, bool create = false)
        {
            this.m_stream = initFromFile(filepath, create);
        }

        public BigEndianWriter(Stream stream)
        {
            this.m_stream = stream;
        }


        public long Position => m_stream.Position;
        public long BytesAvailable => m_stream.Length - m_stream.Position;


        private FileStream initFromFile(string path, bool create)
        {
            var fileMode = create ? FileMode.OpenOrCreate : FileMode.Open;
            var fileStream = new FileStream(path, fileMode, FileAccess.Write);
            return fileStream;
        }


        public void Seek(int offset, SeekOrigin origin) => this.m_stream.Seek(offset, origin);

        public void WriteByte(byte value) => this.m_stream.WriteByte(value);

        public byte[] WriteBytes(int n)
        {
            var buffer = new byte[n];
            this.m_stream.Write(buffer, 0, n);

            return buffer;
        }

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
            var length = (ushort)utf.Length;
            var content = Encoding.UTF8.GetBytes(utf);

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
