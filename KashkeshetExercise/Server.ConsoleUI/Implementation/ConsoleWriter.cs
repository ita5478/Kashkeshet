using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

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
