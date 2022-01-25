using Client.BL.Abstractions;
using Client.BL.Implementation;
using Kashkeshet.Common.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ConsoleUI
{
    public class Bootstrapper
    {
        public ServerConnectionInitializer Initialize()
        {
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var headerParser = new HeadersParser();

            return new ServerConnectionInitializer(stringToByteArrayConverter, headerParser);
        }
    }
}
