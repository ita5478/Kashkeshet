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
        private IConverter<string, byte[]> _stringToByteArrayConverter;

        public ServerNotificationsListener(
            IConverter<ChatMessage, KTPPacket> messageToPacketConverter,
            IWriter<string> writer,
            IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _messageToPacketConverter = messageToPacketConverter;
            _writer = writer;
            _stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public async Task ListenForServerNotifications(IReaderAsync<KTPPacket> packetsReader)
        {
            isListening = true;

            while (isListening)
            {
                var packet = await packetsReader.ReadAsync();

                if (packet.PacketType is KTPPacketType.PUSH)
                {
                    switch (packet.Headers["Event-Type"])
                    {
                        case "new-direct":
                        case "new-message":
                            var message = _messageToPacketConverter.ConvertFrom(packet);
                            _writer.Write($"({message.ChatName}) {message.Sender}: {message.Content}");
                            break;
                        case "user-joined":
                        case "user-left":
                            var announcement = _stringToByteArrayConverter.ConvertFrom(packet.Content);
                            _writer.Write(announcement);
                            break;
                    }
                }
            }
        }
    }
}
