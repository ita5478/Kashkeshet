using Client.BL.Abstractions;
using Client.BL.Exceptions;
using Client.BL.Implementation;
using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.Implementations;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Net;

namespace Client.ConsoleUI
{
    internal class Program
    {
        const string NAME = "user1";
        static void Main(string[] args)
        {
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var headerParser = new HeadersParser();

            var initializer = new ServerConnectionInitializer(stringToByteArrayConverter, headerParser);

            try
            {
                var socketStream = initializer.ConnectAsync(IPAddress.Parse("127.0.0.1"), 6666, NAME).GetAwaiter().GetResult();
            }
            catch (ServerConnectionFailureException)
            {

            }

            Console.Read();
        }
    }
}
