using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.Common.Abstractions
{
    public interface IReaderAsync <T>
    {
        Task<T> ReadAsync();
    }
}
