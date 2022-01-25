using Kashkeshet.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IConnectionInitializer
    {
        public Task<ISocketStream> ConnectAsync(IPAddress ip, int port, string username);
    }
}
