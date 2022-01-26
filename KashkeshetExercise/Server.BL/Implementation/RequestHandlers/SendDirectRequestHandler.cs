using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BL.Implementation.RequestHandlers
{
    public class SendDirectRequestHandler : IRequestHandler
    {
        private IMessageBroadcaster _messageBroadcaster;
        private IUserRegistry _userRegistry;
        private IConverter<ChatMessage, KTPPacket> _messageToPacketConverter;

        public SendDirectRequestHandler(
            IMessageBroadcaster messageBroadcaster,
            IUserRegistry userRegistry,
            IConverter<ChatMessage, KTPPacket> messageToPacketConverter)
        {
            _messageBroadcaster = messageBroadcaster;
            _userRegistry = userRegistry;
            _messageToPacketConverter = messageToPacketConverter;
        }

        public Task HandleRequest(KTPPacket requestPacket)
        {
            var message = _messageToPacketConverter.ConvertFrom(requestPacket);

            string targetUser = requestPacket.Headers["Target-User"];
            List<IWriterAsync<KTPPacket>> recipient = new List<IWriterAsync<KTPPacket>>
            { _userRegistry.GetUserHandler(targetUser).GetClientWriter() };

            var packet = _messageToPacketConverter.ConvertTo(message);
            _messageBroadcaster.BroadcastMessage(packet, recipient);
            return Task.CompletedTask;
        }
    }
}
