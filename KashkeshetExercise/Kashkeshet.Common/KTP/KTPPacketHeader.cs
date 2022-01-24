using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.KTP
{
    public class KTPPacketHeader
    {
        public readonly KTPPacketType PacketType;

        public readonly IDictionary<string, string> Headers;

        public KTPPacketHeader(KTPPacketType packetType, IDictionary<string, string> headers, byte[] content = null)
        {
            PacketType = packetType;
            Headers = headers;
        }
    }
}
