using Kashkeshet.Common.Abstractions;
using System;

namespace Server.ConsoleUI.Implementation
{
    public class ConsoleWriter : IWriter<string>
    {
        public void Write(string data)
        {
            Console.WriteLine(data);
        }
    }
}
