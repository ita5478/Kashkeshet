using System.Threading.Tasks;

namespace Kashkeshet.Common.Abstractions
{
    public interface ISocketStream
    {
        Task<byte[]> ReadAsync(int bufferSize);

        Task WriteAsync(byte[] buffer);

        void Close();
    }
}
