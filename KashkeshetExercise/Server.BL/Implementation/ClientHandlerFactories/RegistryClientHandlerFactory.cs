using Kashkeshet.Common.Abstractions;
using Server.BL.Abstractions;
using System.Collections.Generic;

namespace Server.BL.Implementation
{
    public class RegistryClientHandlerFactory : IClientHandlerFactory
    {
        private IParser<IDictionary<string, string>> _headersParser;
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IClientHandlerFactory _clientHandlerFactory;
        private IUserRegistry _usersRegistry;
        private IWriter<string> _writer;

        public RegistryClientHandlerFactory(
            IParser<IDictionary<string, string>> headersParser,
            IConverter<string, byte[]> stringToByteArrayConverter,
            IClientHandlerFactory clientHandlerFactory,
            IUserRegistry usersRegistry,
            IWriter<string> writer)
        {
            _headersParser = headersParser;
            _stringToByteArrayConverter = stringToByteArrayConverter;
            _clientHandlerFactory = clientHandlerFactory;
            _usersRegistry = usersRegistry;
            _writer = writer;
        }

        public IClientHandler Create(ISocketStream clientStream)
        {
            return new RegisteryClientHandler(_usersRegistry, _writer, clientStream, _clientHandlerFactory, _headersParser, _stringToByteArrayConverter);
        }
    }
}
