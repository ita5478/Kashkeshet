using Kashkeshet.Common.Implementations;
using Server.BL.Abstractions;
using Server.BL.Implementation;
using System.Collections.Generic;

namespace Server.ConsoleUI
{
    public class Bootstrapper
    {
        public IClientListener Initialize()
        {
            var writer = new Implementation.ConsoleWriter();
            var headersParser = new HeadersParser();
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var registry = new UsersRegistry();

            var requestHandlersDictionary = new Dictionary<string, IRequestHandler>()
            {

            };

            var clientHandlerFactory = new ClientHandlerFactory(requestHandlersDictionary, writer, headersParser, stringToByteArrayConverter);
            var registryHandlerFactory = new RegistryClientHandlerFactory(headersParser, stringToByteArrayConverter, clientHandlerFactory, registry, writer);           
            var listener = new TcpSocketClientListener(writer, registryHandlerFactory);

            return listener;
        }
    }
}
