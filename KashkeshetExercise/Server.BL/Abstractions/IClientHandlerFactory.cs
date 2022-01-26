using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;

namespace Server.BL.Abstractions
{
    public interface IClientHandlerFactory
    {
        public IClientHandler Create(ISocketStream clientStream);
    }
}
