using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.BL.Abstractions
{
    public abstract class ClientSocketBase
    {
        public bool IsConnected { get; protected set; }

        public abstract Task<bool> ConnectAsync(IPAddress address, int port);

        public abstract void Close();

        public abstract Task SendDataAsync(byte[] data);

        public abstract Task<byte[]> ReceiveDataAsync(uint bufferSize);
    }
}
