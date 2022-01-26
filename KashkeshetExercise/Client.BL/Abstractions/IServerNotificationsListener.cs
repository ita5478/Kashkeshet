using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IServerNotificationsListener
    {
        Task ListenForServerNotifications(IReaderAsync<KTPPacket> packetsReader);
    }
}
