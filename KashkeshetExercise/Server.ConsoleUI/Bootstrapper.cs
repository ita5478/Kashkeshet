using Server.BL.Abstractions;
using Server.BL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

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
