using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;

namespace Client.ConsoleUI.Implementations
{
    public class CommandPacketReader : IReader<KTPPacket>
    {
        private IWriter<string> _writer;
        private IReader<string> _reader;
        private IParser<KTPPacket> _commandParser;

        public CommandPacketReader(IWriter<string> writer, IReader<string> reader, IParser<KTPPacket> commandParser)
        {
            _writer = writer;
            _reader = reader;
            _commandParser = commandParser;
        }

        public KTPPacket Read()
        {
            _writer.Write("Enter the command you want to send to the server");
            string command = _reader.Read();

            try
            {
                var packet = _commandParser.Parse(command);
                return packet;
            }
            catch(Exception e)
            {
                _writer.Write(e.Message);
                return null;
            }
        }
    }
}
