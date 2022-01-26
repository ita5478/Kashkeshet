using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.BL.Implementation
{
    public class UserStatusAnnouncer : IUserStatusAnnouncer
    {
        private const string USER_JOINED_FORMAT = "The user {0} has joined the chat!";
        private const string USER_LEFT_FORMAT = "The user {0} has left the chat.";

        private IUserRegistry _userRegistry;
        private MessageBroadcaster _messageBroadcaster;
        private IConverter<string, byte[]> stringToByteArrayConverter;

        public UserStatusAnnouncer(IUserRegistry userRegistry, MessageBroadcaster messageBroadcaster, IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _userRegistry = userRegistry;
            _messageBroadcaster = messageBroadcaster;
            this.stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public void AnnounceConnection(string userName)
        {
            string message = string.Format(USER_JOINED_FORMAT, userName);
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Date", DateTime.Now.ToLongDateString()},
                {"Content-Length", message.Length.ToString()},
                {"Event-Type", "user-joined" }
            };

            SendPacket(new KTPPacket(KTPPacketType.PUSH, headers, stringToByteArrayConverter.ConvertTo(message)));
        }

        public void AnnounceDisconnection(string userName)
        {
            string message = string.Format(USER_LEFT_FORMAT, userName);
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Date", DateTime.Now.ToLongDateString()},
                {"Content-Length", message.Length.ToString()},
                {"Event-Type", "user-left" }
            };

            SendPacket(new KTPPacket(KTPPacketType.PUSH, headers, stringToByteArrayConverter.ConvertTo(message)));
        }

        private void SendPacket(KTPPacket packet)
        {
            List<IWriterAsync<KTPPacket>> usersWriters = new List<IWriterAsync<KTPPacket>>();
            foreach(var user in _userRegistry.GetAllUsers())
            {
                usersWriters.Add(_userRegistry.GetUserHandler(user).GetClientWriter());
            }

            _messageBroadcaster.BroadcastMessage(packet, usersWriters);
        }
    }
}
