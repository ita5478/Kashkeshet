using Kashkeshet.Common.Abstractions;
using System.Net;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IConnectionInitializer
    {
        public Task<ISocketStream> ConnectAsync(IPAddress ip, int port, string username);
    }
}
