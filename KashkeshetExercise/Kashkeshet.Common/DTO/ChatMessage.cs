using System;

namespace Kashkeshet.Common.DTO
{
    public class ChatMessage
    {
        public readonly string Sender;
        public readonly string ChatName;
        public readonly DateTime TimeOfSending;
        public readonly string Content;

        public ChatMessage(string sender, string chatName, DateTime timeOfSending, string content)
        {
            Sender = sender;
            ChatName = chatName;
            TimeOfSending = timeOfSending;
            Content = content;
        }
    }
}
