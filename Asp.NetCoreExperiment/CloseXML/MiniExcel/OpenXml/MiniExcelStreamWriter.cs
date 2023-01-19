﻿using System;
using System.IO;
using System.Text;

namespace MiniExcelLibs.OpenXml
{
    public class MiniExcelStreamWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly Encoding _encoding;
        internal readonly StreamWriter _streamWriter;
        private bool disposedValue;
        public MiniExcelStreamWriter(Stream stream, Encoding encoding, int bufferSize)
        {
            this._stream = stream;
            this._encoding = encoding;
            this._streamWriter = new StreamWriter(stream, this._encoding, bufferSize);
        }
        public void Write(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;
            this._streamWriter.Write(content);
        }

        public long WriteAndFlush(string content)
        {
            this.Write(content);
            this._streamWriter.Flush();
            return this._streamWriter.BaseStream.Position;
        }

        public void SetPosition(long position)
        {
            this._streamWriter.BaseStream.Position = position;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this._streamWriter?.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
