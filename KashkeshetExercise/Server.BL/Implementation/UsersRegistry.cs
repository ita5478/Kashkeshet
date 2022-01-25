using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.Implementations;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class UsersRegistry : IUserRegistry
    {
        private ConcurrentDictionary<string, IClientHandler> _registeredClients;
        private IParser<IDictionary<string, string>> _headersParser;
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IWriter<string> _writer;
        public UsersRegistry(IWriter<string> writer)
        {
            _registeredClients = new ConcurrentDictionary<string, IClientHandler>();
            _headersParser = new HeadersParser();
            _stringToByteArrayConverter = new StringToByteArrayConverter();
            _writer = writer;
        }

        public ISocketStream GetUserStream(string userName)
        {
            throw new NotImplementedException();
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
                        _registeredClients[username] = null;
                        registryCompleted = true;
                        headers["Response-Type"] = "acknowledgement";
                        headers["Response-Message"] = $"The user {username} was succussfully registered.";
                        _writer.Write($"A new user by the name {username} has connected!");
                        await packetWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));
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
    }
}
