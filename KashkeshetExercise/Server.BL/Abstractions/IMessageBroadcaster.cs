using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Collections.Generic;

namespace Server.BL.Abstractions
{
    public interface IMessageBroadcaster
    {
        void BroadcastMessage(KTPPacket message, IEnumerable<IWriterAsync<KTPPacket>> recipients);
    }
}
