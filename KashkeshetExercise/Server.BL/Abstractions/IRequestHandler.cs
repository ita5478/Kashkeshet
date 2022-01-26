using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IRequestHandler
    {
        Task HandleRequest(KTPPacket requestPacket);
    }
}
