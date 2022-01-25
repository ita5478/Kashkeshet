using Kashkeshet.Common.Abstractions;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IUserRegistry
    {
        Task Register(ISocketStream userStream);

        ISocketStream GetUserStream(string userName);
    }
}
