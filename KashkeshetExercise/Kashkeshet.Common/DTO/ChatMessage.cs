using Kashkeshet.Common.Enums;
using System;

namespace Kashkeshet.Common.DTO
{
    public class ChatMessage
    {
        public readonly string Sender;
        public readonly string ChatName;
        public readonly DateTime TimeOfSending;
        public readonly byte[] Content;
        public readonly ContentType ContentType;

        public ChatMessage(string sender, string chatName, DateTime timeOfSending, byte[] content, ContentType contentType)
        {
            Sender = sender;
            ChatName = chatName;
            TimeOfSending = timeOfSending;
            Content = content;
            ContentType = contentType;
        }
    }
}
