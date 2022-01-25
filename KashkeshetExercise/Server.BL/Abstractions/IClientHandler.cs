using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IClientHandler
    {
        Task HandleClient();
    }
}
