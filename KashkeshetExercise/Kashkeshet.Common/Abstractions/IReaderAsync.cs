using System.Threading.Tasks;

namespace Kashkeshet.Common.Abstractions
{
    public interface IReaderAsync<T>
    {
        Task<T> ReadAsync();
    }
}
