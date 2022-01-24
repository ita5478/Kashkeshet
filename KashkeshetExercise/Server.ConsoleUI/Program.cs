using System;

namespace Server.ConsoleUI
{
    internal class Program
    {
        const int PORT = 6666;
        static void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();

            var listener = boot.Initialize();

            listener.ListenForClients(PORT).GetAwaiter().GetResult();
        }
    }
}
