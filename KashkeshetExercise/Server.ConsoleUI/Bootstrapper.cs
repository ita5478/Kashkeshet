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
            var requestHandlersDictionary = new Dictionary<string, IRequestHandler>()
            {

            };
            var clientHandlerFactory = new ClientHandlerFactory(requestHandlersDictionary, writer);
           
            var registry = new UsersRegistry(clientHandlerFactory, writer);
            var listener = new TcpSocketClientListener(writer, registry);

            return listener;
        }
    }
}
