using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IUserRegistry
    {
        Task Register(ISocketStream userStream);

        IWriterAsync<KTPPacket> GetUserWriter(string userName);
    }
}
