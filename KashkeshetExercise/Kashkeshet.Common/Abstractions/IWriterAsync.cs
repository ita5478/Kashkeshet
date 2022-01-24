using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.Common.Abstractions
{
    public interface IWriterAsync <T>
    {
        Task WriteAsync(T data);
    }
}
