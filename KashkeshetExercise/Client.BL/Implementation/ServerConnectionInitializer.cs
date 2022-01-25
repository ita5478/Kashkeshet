using Client.BL.Abstractions;
using Kashkeshet.Common.Abstractions;
using Server.BL.Implementation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.BL.Implementation
{
    public class ServerConnectionInitializer : IConnectionInitializer
    {
        public async Task<ISocketStream> ConnectAsync(IPAddress ip, int port)
        {
            TcpClient tcpClient = new TcpClient();

            await tcpClient.ConnectAsync(ip, port);

            return new TcpSocketStream(tcpClient.GetStream());
        }
    }
}
