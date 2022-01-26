using Kashkeshet.Common.Abstractions;
using System;

namespace Client.ConsoleUI.Implementations
{
    public class ConsoleReader : IReader<string>
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
