using Client.BL.Abstractions;
using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Client.BL.Implementation
{
    public class ServerNotificationsListener : IServerNotificationsListener
    {
        private bool isListening;
        private IConverter<ChatMessage, KTPPacket> _messageToPacketConverter;
        private IWriter<string> _writer;

        public ServerNotificationsListener(
            IConverter<ChatMessage, KTPPacket> messageToPacketConverter,
            IWriter<string> writer)
        {
            _messageToPacketConverter = messageToPacketConverter;
            _writer = writer;
        }

        public async Task ListenForServerNotifications(IReaderAsync<KTPPacket> packetsReader)
        {
            isListening = true;

            while (isListening)
            {
                var packet = await packetsReader.ReadAsync();

                if (packet.PacketType is KTPPacketType.PUSH)
                {
                    var message = _messageToPacketConverter.ConvertFrom(packet);
                    _writer.Write($"({message.ChatName}) {message.Sender}: {message.Content}");
                }
            }
        }
    }
}
