using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.Implementations;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class UsersRegistry : IUserRegistry
    {
        private ConcurrentDictionary<string, ClientHandler> _registeredClients;
        private IParser<IDictionary<string, string>> _headersParser;
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IWriter<string> _writer;
        private IClientHandlerFactory _clientHandlerFactory;

        public UsersRegistry(IClientHandlerFactory clientHandlerFactory, IWriter<string> writer)
        {
            _registeredClients = new ConcurrentDictionary<string, ClientHandler>();
            _headersParser = new HeadersParser();
            _stringToByteArrayConverter = new StringToByteArrayConverter();
            _writer = writer;
            _clientHandlerFactory = clientHandlerFactory;
        }

        public IWriterAsync<KTPPacket> GetUserWriter(string userName)
        {
            return _registeredClients[userName].PacketsWriter;
        }

        public async Task Register(ISocketStream userStream)
        {
            var packetWriter = new KTPPacketWriter(userStream, _stringToByteArrayConverter);
            var packetReader = new KTPPacketReader(userStream, _stringToByteArrayConverter, _headersParser);

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Request-Type", "registry-identification" }
            };

            await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.REQ, headers));

            bool registryCompleted = false;
            var retPacket = await packetReader.ReadAsync();
            headers.Clear();

            while (!registryCompleted)
            {
                if (retPacket.PacketType == KTPPacketType.RES)
                {
                    string username = retPacket.Headers["Identify-As"];
                    if (!string.IsNullOrEmpty(username) && !_registeredClients.ContainsKey(retPacket.Headers["Identify-As"]))
                    {
                        registryCompleted = true;
                        headers["Response-Type"] = "acknowledgement";
                        headers["Response-Message"] = $"The user {username} was succussfully registered.";
                        _writer.Write($"A new user by the name {username} has connected!");
                        await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));

                        _registeredClients[username] = (ClientHandler)_clientHandlerFactory.Create(packetWriter, packetReader, username);
                        StartClientHandler(username).Start();
                    }
                    else
                    {
                        headers["Response-Type"] = "error";
                        headers["Response-Message"] = $"A user by the name {username} is already connected.";
                        await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));
                        userStream.Close();
                    }
                }
            }
        }

        private async Task StartClientHandler(string userName)
        {
            await _registeredClients[userName].HandleClient();
            ClientHandler clientHandler;
            while(!_registeredClients.TryRemove(userName, out clientHandler))
            {
                await Task.Delay(100);
            }
        }
    }
}
