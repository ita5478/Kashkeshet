using Server.BL.Abstractions;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class TcpSocketStream : ISocketStream
    {
        private NetworkStream _socketStream;

        public TcpSocketStream(NetworkStream networkStream)
        {
            _socketStream = networkStream;
        }

        public void Close()
        {
            _socketStream.Close();
            TcpClient client = new TcpClient();
        }

        public async Task<byte[]> ReadAsync(int bufferSize)
        {
            byte[] bytes = new byte[bufferSize];
            int amountReceived = await _socketStream.ReadAsync(bytes, 0, bufferSize);
            var ret = new byte[amountReceived];
            System.Array.Copy(bytes, ret, amountReceived);
            return ret;
        }

        public async Task WriteAsync(byte[] buffer)
        {
            await _socketStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
