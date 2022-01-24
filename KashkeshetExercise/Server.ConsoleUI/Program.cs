using System;

namespace Server.ConsoleUI
{
    internal class Program
    {
        const int PORT = 6666;
        static async void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();

            var listener = boot.Initialize();

            await listener.ListenForClients(PORT);
        }
    }
}
