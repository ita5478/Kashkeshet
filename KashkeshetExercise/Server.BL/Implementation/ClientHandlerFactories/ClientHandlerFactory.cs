using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.BL.Implementation
{
    public class ClientHandlerFactory : IClientHandlerFactory
    {
        private IDictionary<string, IRequestHandler> _requestHandlers;
        private IWriter<string> _outputWriter;
        private IParser<IDictionary<string, string>> _headersParser;
        private IConverter<string, byte[]> _stringToByteArrayConverter;

        public ClientHandlerFactory(
            IDictionary<string, IRequestHandler> requestHandlers, 
            IWriter<string> outputWriter, 
            IParser<IDictionary<string, string>> headersParser, 
            IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _requestHandlers = requestHandlers;
            _outputWriter = outputWriter;
            _headersParser = headersParser;
            _stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public IClientHandler Create(ISocketStream clientStream)
        {
            var packetsWriter = new KTPPacketWriter(clientStream, _stringToByteArrayConverter);
            var packetsReader = new KTPPacketReader(clientStream, _stringToByteArrayConverter, _headersParser);

            return new ClientHandler(packetsWriter, packetsReader, _requestHandlers);
        }
    }
}
