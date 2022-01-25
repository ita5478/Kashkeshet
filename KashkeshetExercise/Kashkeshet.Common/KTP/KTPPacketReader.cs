using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.Common.KTP
{
    public class KTPPacketReader : IReaderAsync<KTPPacket>
    {
        private ISocketStream _socketStream;
        private IConverter<string, byte[]> _stringToByteArrayConverter;
        private IParser<IDictionary<string, string>> _headersParser;

        public KTPPacketReader(
            ISocketStream socketStream, 
            IConverter<string, byte[]> stringToByteArrayConverter, 
            IParser<IDictionary<string, string>> headersParser)
        {
            _socketStream = socketStream;
            _stringToByteArrayConverter = stringToByteArrayConverter;
            _headersParser = headersParser;
        }

        public async Task<KTPPacket> ReadAsync()
        {
            var mainHeader = await ReadMainHeaderAsync();

            mainHeader =  mainHeader.Replace("\r\n", String.Empty);
            var partsOfHeader = mainHeader.Split(' ');

            KTPPacketType packetType = (KTPPacketType) Enum.Parse(typeof(KTPPacketType), partsOfHeader[0]);
            int headersLength = int.Parse(partsOfHeader[1]);

            var bytes = await _socketStream.ReadAsync(headersLength);
            string a = _stringToByteArrayConverter.ConvertFrom(bytes);
            var headers = _headersParser.Parse(a);

            return new KTPPacket (packetType, headers, await ReadContentBytesAsync(headers));
        }

        private async Task<string> ReadMainHeaderAsync()
        {
            bool keepReading = true;
            string header = string.Empty;
            byte[] bytesReceived;

            while (keepReading)
            {
                bytesReceived = await _socketStream.ReadAsync(1);

                header += _stringToByteArrayConverter.ConvertFrom(bytesReceived);

                if (header.EndsWith("\r\n"))
                {
                    keepReading = false;
                }
            }

            return header;
        }

        private async Task<byte[]> ReadContentBytesAsync(IDictionary<string, string> headers)
        {
            byte[] bytes = null;

            if (headers.ContainsKey("Content-Length"))
            {
                int contentLength = int.Parse(headers["Content-Length"]);

                bytes = await _socketStream.ReadAsync(contentLength);
            }

            return bytes;
        }
    }
}
