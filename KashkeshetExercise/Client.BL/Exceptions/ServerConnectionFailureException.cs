using System;
using System.Collections.Generic;
using System.Text;

namespace Client.BL.Exceptions
{
    public class ServerConnectionFailureException : Exception
    {
        public ServerConnectionFailureException() : base() { }

        public ServerConnectionFailureException(string message) : base(message) { }
    }
}
