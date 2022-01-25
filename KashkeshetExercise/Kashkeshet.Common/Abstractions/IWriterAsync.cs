using System.Threading.Tasks;

namespace Kashkeshet.Common.Abstractions
{
    public interface IWriterAsync<T>
    {
        Task WriteAsync(T data);
    }
}
