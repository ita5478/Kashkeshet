using Client.BL.Abstractions;
using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Client.BL.Implementation
{
    public class ClientRunner : IClientRunner
    {
        private IConnectionInitializer _connectionInitializer;
        private IServerNotificationsListener _serverNotificationsListener;
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IParser<IDictionary<string, string>> _headersParser;
        private IReader<KTPPacket> _inputReader;

        public ClientRunner(
            IConnectionInitializer connectionInitializer,
            IServerNotificationsListener serverNotificationsListener,
            IConverter<string, byte[]> stringToByteArrayConverter,
            IParser<IDictionary<string, string>> headersParser,
            IReader<KTPPacket> inputReader)
        {
            _connectionInitializer = connectionInitializer;
            _serverNotificationsListener = serverNotificationsListener;
            _stringToByteArrayConverter = stringToByteArrayConverter;
            _headersParser = headersParser;
            _inputReader = inputReader;
        }

        public async Task Start(IPAddress address, int port, string username)
        {
            var socket = await _connectionInitializer.ConnectAsync(address, port, username);
            _serverNotificationsListener.ListenForServerNotifications(new KTPPacketReader(socket, _stringToByteArrayConverter, _headersParser));

            var packetWriter = new KTPPacketWriter(socket, _stringToByteArrayConverter);

            while (true)
            {
                var packet = _inputReader.Read();
                packet.Headers["Sender"] = username;
                await packetWriter.WriteAsync(packet);
            }
        }
    }
}
