using Server.BL.Abstractions;
using Server.BL.Implementation;

namespace Server.ConsoleUI
{
    public class Bootstrapper
    {
        public IClientListener Initialize()
        {
            var writer = new Implementation.ConsoleWriter();
            var registry = new UsersRegistry(writer);
            var listener = new TcpSocketClientListener(writer, registry);

            return listener;
        }
    }
}
