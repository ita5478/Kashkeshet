using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IServerNotificationsListener
    {
        Task ListenForServerNotifications(IReaderAsync<KTPPacket> packetsReader);
    }
}
