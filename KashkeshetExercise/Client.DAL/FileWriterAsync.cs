using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL
{
    public class FileWriterAsync : IWriterAsync<byte[]>, IAsyncDisposable
    {
        private FileStream _fileStream;

        public FileWriterAsync(string filepath, FileMode mode)
        {
            _fileStream = new FileStream(filepath, mode);
        }

        public async Task WriteAsync(byte[] data)
        {
            await _fileStream.WriteAsync(data);
        }

        public async ValueTask DisposeAsync()
        {
            await _fileStream.FlushAsync();
            await _fileStream.DisposeAsync();
        }
    }
}
