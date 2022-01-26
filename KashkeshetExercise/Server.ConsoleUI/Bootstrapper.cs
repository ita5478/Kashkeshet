using Kashkeshet.Common.Implementations;
using Server.BL.Abstractions;
using Server.BL.Implementation;
using Server.BL.Implementation.RequestHandlers;
using System.Collections.Generic;

namespace Server.ConsoleUI
{
    public class Bootstrapper
    {
        public IClientListener Initialize()
        {
            // Converters
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var chatMessageToPacketConverter = new ChatMessageToPacketConverter(stringToByteArrayConverter);

            var writer = new Implementation.ConsoleWriter();
            var headersParser = new HeadersParser();

            var registry = new UsersRegistry();
            var messageBroadcaster = new MessageBroadcaster();
            var statusAnnouncer = new UserStatusAnnouncer(registry, messageBroadcaster, stringToByteArrayConverter);

            var requestHandlersDictionary = new Dictionary<string, IRequestHandler>()
            {
                {"send", new SendBroadcastRequestHandler(messageBroadcaster, registry, chatMessageToPacketConverter) }
            };

            var clientHandlerFactory = new ClientHandlerFactory(requestHandlersDictionary, writer, headersParser, stringToByteArrayConverter);
            var registryHandlerFactory = new RegistryClientHandlerFactory(
                headersParser,
                stringToByteArrayConverter,
                clientHandlerFactory,
                registry,
                writer,
                statusAnnouncer);

            var listener = new TcpSocketClientListener(writer, registryHandlerFactory);

            return listener;
        }
    }
}
