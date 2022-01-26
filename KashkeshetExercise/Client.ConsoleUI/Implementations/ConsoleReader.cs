using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

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
