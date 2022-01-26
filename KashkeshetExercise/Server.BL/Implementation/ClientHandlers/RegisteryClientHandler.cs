using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class RegisteryClientHandler : IClientHandler
    {
        private IUserRegistry _usersRegistry;
        private IWriter<string> _writer;
        private ISocketStream _socketStream;
        private IClientHandlerFactory _clientHandlerFactory;
        private IParser<IDictionary<string, string>> _headersParser;
        private IConverter<string, byte[]> _stringToByteArrayConverter;

        public RegisteryClientHandler(
            IUserRegistry usersRegistry, 
            IWriter<string> writer, 
            ISocketStream socketStream, 
            IClientHandlerFactory clientHandlerFactory, 
            IParser<IDictionary<string, string>> headersParser, 
            IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _usersRegistry = usersRegistry;
            _writer = writer;
            _socketStream = socketStream;
            _clientHandlerFactory = clientHandlerFactory;
            _headersParser = headersParser;
            _stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public async Task HandleClient()
        {
            var packetWriter = new KTPPacketWriter(_socketStream, _stringToByteArrayConverter);
            var packetReader = new KTPPacketReader(_socketStream, _stringToByteArrayConverter, _headersParser);

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Request-Type", "registry-identification" }
            };

            await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.REQ, headers));

            var retPacket = await packetReader.ReadAsync();
            headers.Clear();

            if (retPacket.PacketType == KTPPacketType.RES)
            {
                string username = retPacket.Headers["Identify-As"];
                if (!string.IsNullOrEmpty(username) && !_usersRegistry.IsUserRegistered(retPacket.Headers["Identify-As"]))
                {
                    headers["Response-Type"] = "acknowledgement";
                    headers["Response-Message"] = $"The user {username} was succussfully registered.";
                    _writer.Write($"A new user by the name {username} has connected!");
                    await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));

                    var handler = _clientHandlerFactory.Create(_socketStream);
                    _usersRegistry.Register(username, handler);
                    StartClientHandler(username).Start();
                }
                else
                {
                    headers["Response-Type"] = "error";
                    headers["Response-Message"] = $"A user by the name {username} is already connected.";
                    await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));
                    _socketStream.Close();
                }

            }
        }

        private async Task StartClientHandler(string userName)
        {
            await _usersRegistry.GetUserHandler(userName).HandleClient();
            while (_usersRegistry.IsUserRegistered(userName) && !_usersRegistry.Unregister(userName))
            {
                await Task.Delay(100);
            }
        }

        public IWriterAsync<KTPPacket> GetClientWriter()
        {
            return null;
        }
    }
}
