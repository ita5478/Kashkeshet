using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL
{
    public class FileReaderAsync : IReaderAsync<byte[]>, IAsyncDisposable
    {
        FileStream _fileStream;

        public FileReaderAsync(string filepath)
        {
            _fileStream = new FileStream(filepath, FileMode.Open);
        }

        public async ValueTask DisposeAsync()
        {
            await _fileStream.FlushAsync();
            await _fileStream.DisposeAsync();
        }

        public async Task<byte[]> ReadAsync()
        {
            byte[] buffer = new byte[_fileStream.Length];
            await _fileStream.ReadAsync(buffer);
            return buffer;
        }
    }
}
