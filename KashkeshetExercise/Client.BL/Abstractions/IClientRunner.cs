using System.Net;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IClientRunner
    {
        Task Start(IPAddress address, int port, string username);
    }
}
