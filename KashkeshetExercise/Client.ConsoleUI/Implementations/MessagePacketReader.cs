using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ConsoleUI.Implementations
{
    public class MessagePacketReader : IReader<KTPPacket>
    {
        private IWriter<string> _writer;
        private IReader<string> _reader;
        private IConverter<string, byte[]> _stringToByteArrayConverter;

        public MessagePacketReader(IWriter<string> writer, IReader<string> reader, IConverter<string, byte[]> stringToByteArrayConverter)
        {
            _writer = writer;
            _reader = reader;
            _stringToByteArrayConverter = stringToByteArrayConverter;
        }

        public KTPPacket Read()
        {
            _writer.Write("Enter the message you want to send:");
            string message = _reader.Read();

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Date", DateTime.Now.ToLongDateString() },
                {"Request-Type", "send" },
                {"Content-Length", message.Length.ToString()}
            };

            return new KTPPacket(KTPPacketType.REQ, headers, _stringToByteArrayConverter.ConvertTo(message));
        }
    }
}
