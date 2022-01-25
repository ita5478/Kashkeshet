using Client.BL.Implementation;
using Kashkeshet.Common.Implementations;

namespace Client.ConsoleUI
{
    public class Bootstrapper
    {
        public ServerConnectionInitializer Initialize()
        {
            var stringToByteArrayConverter = new StringToByteArrayConverter();
            var headerParser = new HeadersParser();

            return new ServerConnectionInitializer(stringToByteArrayConverter, headerParser);
        }
    }
}
