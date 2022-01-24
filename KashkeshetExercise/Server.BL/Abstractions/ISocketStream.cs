using System.Threading.Tasks;

namespace Server.Core.BL.Abstractions
{
    public interface ISocketStream
    {
        Task<byte[]> ReadAsync(int bufferSize);

        Task WriteAsync(byte[] buffer);

        void Close();
    }
}
