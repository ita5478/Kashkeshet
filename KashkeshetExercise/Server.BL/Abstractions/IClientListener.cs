using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IClientListener
    {
        public Task ListenForClients(int port);
    }
}
