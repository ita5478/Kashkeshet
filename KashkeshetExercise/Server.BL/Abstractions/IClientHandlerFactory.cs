using Kashkeshet.Common.Abstractions;

namespace Server.BL.Abstractions
{
    public interface IClientHandlerFactory
    {
        public IClientHandler Create(ISocketStream clientStream);
    }
}
