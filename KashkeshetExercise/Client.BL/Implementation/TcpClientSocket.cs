using Client.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.BL.Implementation
{
    public class TcpClientSocket : ClientSocketBase
    {
        private readonly TcpClient _socket;

        public TcpClientSocket()
        {
            _socket = new TcpClient();
            IsConnected = false;
        }

        public override void Close()
        {
            IsConnected = false;
            _socket.Close();
        }

        public override async Task<bool> ConnectAsync(IPAddress address, int port)
        {
            try
            {
                await _socket.ConnectAsync(address, port);
            }
            catch (SocketException)
            {
                return false;
                // log
            }
            catch (ObjectDisposedException)
            {
                return false;
                // log
            }

            return true;
        }

        public override async Task<byte[]> ReceiveDataAsync(uint bufferSize)
        {
            try
            {
                byte[] bytes = new byte[bufferSize];
                await _socket.GetStream().ReadAsync(bytes, 0, (int)bufferSize);
                return bytes;
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        public override async Task SendDataAsync(byte[] data)
        {
            try
            {
                await _socket.GetStream().WriteAsync(data);
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }
    }
}
