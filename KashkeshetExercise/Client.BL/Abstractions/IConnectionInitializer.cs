using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public interface IConnectionInitializer
    {
        public Task<bool> ConnectAsync(IPAddress ip, int port, string username);
    }
}
