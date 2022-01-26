using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.Enums;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;

namespace Kashkeshet.Common.Implementations
{
    public class ChatMessageToPacketConverter : IConverter<ChatMessage, KTPPacket>
    {
        public ChatMessage ConvertFrom(KTPPacket input)
        {
            try
            {
                string channel = string.Empty;
                if (input.PacketType is KTPPacketType.REQ)
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
                ContentType contentType = (ContentType) Enum.Parse(typeof(ContentType), input.Headers["Content-Type"]);
                DateTime time = DateTime.Parse(input.Headers["Date"]);
                return new ChatMessage(sender, channel, time, input.Content, contentType);
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
                {"Content-Length", input.Content.Length.ToString()},
                {"Content-Type", input.ContentType.ToString() }
            };

            if (input.ChatName.Equals("Direct"))
            {
                headers["Event-Type"] = "new-direct";
            }
            else
            {
                headers["Event-Type"] = "new-message";
            }

            return new KTPPacket(KTPPacketType.PUSH, headers, input.Content);
        }
    }
}
