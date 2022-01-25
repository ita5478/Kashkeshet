using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class ClientHandler : IClientHandler
    {
        public bool IsActive { get; private set; }
        public IWriterAsync<KTPPacket> PacketsWriter;
        
        private string _userName;
        private IReaderAsync<KTPPacket> _packetsReader;
        private IDictionary<string, IRequestHandler> _requestHandlingDictionary;

        public ClientHandler(
            string userName, 
            IWriterAsync<KTPPacket> packetsWriter, 
            IReaderAsync<KTPPacket> packetsReader, 
            IDictionary<string, IRequestHandler> requestHandlingDictionary)
        {
            _userName = userName;
            PacketsWriter = packetsWriter;
            _packetsReader = packetsReader;
            _requestHandlingDictionary = requestHandlingDictionary;
        }

        public async Task HandleClient()
        {
            // important - HandleClient should not leak any errors!
            IsActive = true;

            while (IsActive)
            {
                var incomingPacket = await _packetsReader.ReadAsync();

                if(incomingPacket.PacketType is KTPPacketType.REQ)
                {
                    try
                    {
                        await _requestHandlingDictionary[incomingPacket.Headers["Request-Type"]].HandleRequest(incomingPacket);
                    }
                    catch (KeyNotFoundException)
                    {
                        await SendErrorResponse("Invalid request");
                    }
                }
            }
        }

        private async Task SendErrorResponse(string errorMessage)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
                        {
                            {"Response-Type", "error" },
                            {"Error-Message", errorMessage }
                        };

            await PacketsWriter.WriteAsync(new KTPPacket(KTPPacketType.RES, headers));
        }
    }
}
