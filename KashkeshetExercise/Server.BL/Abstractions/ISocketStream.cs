using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface ISocketStream
    {
        Task<byte[]> ReadAsync(int bufferSize);

        Task WriteAsync(byte[] buffer);

        void Close();
    }
}
