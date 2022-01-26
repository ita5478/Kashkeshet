using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IClientHandler
    {
        Task HandleClient();

        IWriterAsync<KTPPacket> GetClientWriter();
    }
}
