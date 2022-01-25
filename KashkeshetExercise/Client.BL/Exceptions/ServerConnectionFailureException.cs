using System;

namespace Client.BL.Exceptions
{
    public class ServerConnectionFailureException : Exception
    {
        public ServerConnectionFailureException() : base() { }

        public ServerConnectionFailureException(string message) : base(message) { }
    }
}
