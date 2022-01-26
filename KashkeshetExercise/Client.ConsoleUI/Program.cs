using Client.BL.Exceptions;
using System;
using System.Net;

namespace Client.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();
            var clientRunner = boot.Initialize();

            try
            {
                clientRunner.Start(IPAddress.Parse("127.0.0.1"), 6666, args[0]).GetAwaiter().GetResult();
            }
            catch (ServerConnectionFailureException)
            {

            }

            Console.Read();
        }
    }
}
