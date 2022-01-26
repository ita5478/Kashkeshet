using Client.BL.Abstractions;
using Client.BL.Implementation;
using Client.ConsoleUI.Implementations;
using Kashkeshet.Common.Implementations;

namespace Client.ConsoleUI
{
    public class Bootstrapper
    {
        public IClientRunner Initialize()
        {
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var messageToPacketConverter = new ChatMessageToPacketConverter(stringToByteArrayConverter);

            var headerParser = new HeadersParser();

            var consoleWriter = new ConsoleWriter();
            var consoleReader = new ConsoleReader();
            var messagePacketReader = new MessagePacketReader(consoleWriter, consoleReader, stringToByteArrayConverter);

            var initializer = new ServerConnectionInitializer(stringToByteArrayConverter, headerParser);
            var notificationListener = new ServerNotificationsListener(messageToPacketConverter, consoleWriter, stringToByteArrayConverter);

            return new ClientRunner(initializer, notificationListener, stringToByteArrayConverter, headerParser, messagePacketReader);
        }
    }
}
