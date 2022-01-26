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
            var messageToPacketConverter = new ChatMessageToPacketConverter();

            var headerParser = new HeadersParser();
            var commandsParser = new CommandsParser(stringToByteArrayConverter);

            var consoleWriter = new ConsoleWriter();
            var consoleReader = new ConsoleReader();
            var messagePacketReader = new CommandPacketReader(consoleWriter, consoleReader, commandsParser);

            var initializer = new ServerConnectionInitializer(stringToByteArrayConverter, headerParser);
            var notificationListener = new ServerNotificationsListener(messageToPacketConverter, consoleWriter, stringToByteArrayConverter);

            return new ClientRunner(initializer, notificationListener, stringToByteArrayConverter, headerParser, messagePacketReader);
        }
    }
}
