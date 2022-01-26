using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;

namespace Client.ConsoleUI.Implementations
{
    public class CommandsParser : IParser<KTPPacket>
    {
        private IConverter<string, byte[]> _stringToByteArrayConverter;

        public CommandsParser(IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public KTPPacket Parse(string data)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Date", DateTime.Now.ToLongDateString() },
            };

            string messageContent = string.Empty;
            if (data.StartsWith("send"))
            {
                data = data.Substring(4).Trim();
                if (data.StartsWith('<'))
                {
                    headers["Request-Type"] = "send-direct";
                    string targetUser = data.Substring(1, data.IndexOf('>') - 1);
                    headers["Message-Type"] = "direct";
                    headers["Target-User"] = targetUser;
                    messageContent = data.Substring(data.IndexOf('>') + 1);
                }
                else
                {
                    headers["Request-Type"] = "send";
                    headers["Message-Type"] = "general";
                    messageContent = data;
                }
            }
            messageContent = messageContent.Trim();
            headers["Content-Length"] = messageContent.Length.ToString();
            return new KTPPacket(KTPPacketType.REQ, headers, _stringToByteArrayConverter.ConvertTo(messageContent));
        }
    }
}
