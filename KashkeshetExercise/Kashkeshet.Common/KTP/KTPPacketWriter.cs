using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.Common.KTP
{
    public class KTPPacketWriter : IWriterAsync<KTPPacket>
    {
        private ISocketStream _socketStream;
        private IConverter<string, byte[]> _converter;

        public KTPPacketWriter(ISocketStream socketStream, IConverter<string, byte[]> converter)
        {
            _socketStream = socketStream;
            _converter = converter;
        }

        public async Task WriteAsync(KTPPacket data)
        {
            string headers = string.Empty;

            foreach(var header in data.Headers)
            {
                headers += $"{header.Key}:{header.Value}\r\n";
            }

            headers += "\r\n";

            string packet = $"{data.PacketType.ToString()} {headers.Length}\r\n{headers}";
            var packetBytes = _converter.ConvertTo(packet);

            await _socketStream.WriteAsync(packetBytes);
            
            if(data.Content != null  &&  data.Content.Length > 0)
            {
                await _socketStream.WriteAsync(data.Content);
            }
        }
    }
}
