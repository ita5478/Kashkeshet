using Client.BL.Abstractions;
using Client.BL.Exceptions;
using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Implementation;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.BL.Implementation
{
    public class ServerConnectionInitializer : IConnectionInitializer
    {
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IParser<IDictionary<string, string>> _headerParser;

        public ServerConnectionInitializer(IConverter<string, byte[]> stringToByteArrayConverter, IParser<IDictionary<string, string>> headerParser)
        {
            _stringToByteArrayConverter = stringToByteArrayConverter;
            _headerParser = headerParser;
        }

        public async Task<ISocketStream> ConnectAsync(IPAddress ip, int port, string username)
        {
            TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);
            var socketStream = new TcpSocketStream(tcpClient.GetStream());

            var packetReader = new KTPPacketReader(socketStream, _stringToByteArrayConverter, _headerParser);
            var packetWriter = new KTPPacketWriter(socketStream, _stringToByteArrayConverter);

            KTPPacket packet = await packetReader.ReadAsync();

            if (packet.PacketType == KTPPacketType.REQ && packet.Headers["Request-Type"].Contains("registry-identification"))
            {
                KTPPacket outPacket;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers["Identify-As"] = username;

                outPacket = new KTPPacket(KTPPacketType.RES, headers);

                await packetWriter.WriteAsync(outPacket);
                var response = await packetReader.ReadAsync();

                if (response.PacketType == KTPPacketType.RES && response.Headers["Response-Type"].Contains("acknowledgement"))
                {
                    return socketStream;
                }
                else
                {
                    if (response.Headers.ContainsKey("Response-Message"))
                    {
                        throw new ServerConnectionFailureException(response.Headers["Response-Message"]);
                    }
                    else
                    {
                        throw new ServerConnectionFailureException();
                    }

                }
            }
            else
            {
                throw new ServerConnectionFailureException();
            }
        }
    }
}
