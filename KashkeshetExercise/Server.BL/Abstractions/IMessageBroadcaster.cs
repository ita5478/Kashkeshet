using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IMessageBroadcaster
    {
        void BroadcastMessage(KTPPacket message, IEnumerable<IWriterAsync<KTPPacket>> recipients);
    }
}
