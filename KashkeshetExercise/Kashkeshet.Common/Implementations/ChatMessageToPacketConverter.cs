using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;

namespace Kashkeshet.Common.Implementations
{
    public class ChatMessageToPacketConverter : IConverter<ChatMessage, KTPPacket>
    {
        private IConverter<string, byte[]> stringToByteArrayConverter;

        public ChatMessageToPacketConverter(IConverter<string, byte[]> stringToByteArrayConverter)
        {
            this.stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public ChatMessage ConvertFrom(KTPPacket input)
        {
            try
            {
                string channel = string.Empty;
                if(input.PacketType is KTPPacketType.REQ)
                {
                    if (input.Headers["Request-Type"].Equals("send-direct"))
                    {
                        channel = "Direct";
                    }
                    else
                    {
                        channel = "General";
                    }
                }
                else
                {
                    if (input.Headers["Event-Type"].Equals("new-direct"))
                    {
                        channel = "Direct";
                    }
                    else
                    {
                        channel = "General";
                    }
                }
                string sender = input.Headers["Sender"];
                DateTime time = DateTime.Parse(input.Headers["Date"]);
                return new ChatMessage(sender, channel, time, stringToByteArrayConverter.ConvertFrom(input.Content));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public KTPPacket ConvertTo(ChatMessage input)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Sender", input.Sender },
                {"Date", input.TimeOfSending.ToLongDateString()},
                {"Content-Length", input.Content.Length.ToString()}
            };

            if (input.ChatName.Equals("Direct"))
            {
                headers["Event-Type"] = "new-direct";
            }
            else
            {
                headers["Event-Type"] = "new-message";
            }

            return new KTPPacket(KTPPacketType.PUSH, headers, stringToByteArrayConverter.ConvertTo(input.Content));
        }
    }
}
