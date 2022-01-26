using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.DTO;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Text;

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
                string sender = input.Headers["Sender"];
                DateTime time = DateTime.Parse(input.Headers["Date"]);
                return new ChatMessage(sender, "General", time, stringToByteArrayConverter.ConvertFrom(input.Content));
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
                {"Event-Type", "new-message" },
                {"Content-Length", input.Content.Length.ToString()}
            };

            return new KTPPacket(KTPPacketType.PUSH, headers, stringToByteArrayConverter.ConvertTo(input.Content));
        }
    }
}
