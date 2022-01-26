using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BL.Implementation.RequestHandlers
{
    public class SendMessageRequestHandler : IRequestHandler
    {
        private IMessageBroadcaster _messageBroadcaster;
        private IUserRegistry _userRegistry;
        private IConverter<ChatMessage, KTPPacket> _messageToPacketConverter;

        public SendMessageRequestHandler(
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
            List<IWriterAsync<KTPPacket>> recipientWriters = new List<IWriterAsync<KTPPacket>>();

            foreach (var recipient in _userRegistry.GetAllUsers())
            {
                recipientWriters.Add(_userRegistry.GetUserHandler(recipient).GetClientWriter());
            }

            _messageBroadcaster.BroadcastMessage(_messageToPacketConverter.ConvertTo(message), recipientWriters);
            return Task.CompletedTask;
        }
    }
}
