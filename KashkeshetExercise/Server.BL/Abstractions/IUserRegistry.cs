using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.KTP;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IUserRegistry
    {
        void Register(string username, IClientHandler userHandler);

        bool Unregister(string username);

        bool IsUserRegistered(string username);

        IClientHandler GetUserHandler(string userName);
    }
}
