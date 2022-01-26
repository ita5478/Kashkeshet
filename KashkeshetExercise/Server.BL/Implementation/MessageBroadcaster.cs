using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class MessageBroadcaster : IMessageBroadcaster
    {
        public void BroadcastMessage(KTPPacket packet, IEnumerable<IWriterAsync<KTPPacket>> recipients)
        {
            Parallel.ForEach(recipients, async (recipient, i) =>
            {
                await recipient.WriteAsync(packet);
            });
        }
    }
}
