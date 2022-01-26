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

        public ClientHandlerFactory(IDictionary<string, IRequestHandler> requestHandlers, IWriter<string> outputWriter)
        {
            _requestHandlers = requestHandlers;
            _outputWriter = outputWriter;
        }

        public IClientHandler Create(IWriterAsync<KTPPacket> writer, IReaderAsync<KTPPacket> reader, string clientName)
        {
            return new ClientHandler(clientName, writer, reader, _requestHandlers);
        }
    }
}
