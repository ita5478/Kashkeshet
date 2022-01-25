using Kashkeshet.Common.Abstractions;
using Server.BL.Abstractions;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class TcpSocketClientListener : IClientListener
    {
        private IWriter<string> _writer;
        private TcpListener _listener;
        private bool _running;
        private IUserRegistry _userRegistry;

        public TcpSocketClientListener(IWriter<string> writer, IUserRegistry userRegistry)
        {
            _writer = writer;
            _userRegistry = userRegistry;
        }

        public async Task ListenForClients(int port)
        {
            _listener = new TcpListener(IPAddress.Parse("0.0.0.0"), port);

            _writer.Write($"Listening on port {port}.");
            _listener.Start();
            _running = true;

            while (_running)
            {
                var clientStream = new TcpSocketStream((await _listener.AcceptTcpClientAsync()).GetStream());
                _writer.Write("A new connection has been accepted. Starting registry procedure.");
                _userRegistry.Register(clientStream);
            }

        }
    }
}
