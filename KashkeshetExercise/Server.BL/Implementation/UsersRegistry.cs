using Kashkeshet.Common.Abstractions;
using Kashkeshet.Common.Implementations;
using Kashkeshet.Common.KTP;
using Server.BL.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.BL.Implementation
{
    public class UsersRegistry : IUserRegistry
    {
        private ConcurrentDictionary<string, IClientHandler> _registeredClients;
        
        public UsersRegistry()
        {
            _registeredClients = new ConcurrentDictionary<string, IClientHandler>();
        }

        public ICollection<string> GetAllUsers()
        {
            return _registeredClients.Keys;
        }

        public IClientHandler GetUserHandler(string userName)
        {
            return _registeredClients[userName];
        }

        public bool IsUserRegistered(string username)
        {
            return _registeredClients.ContainsKey(username);
        }

        public void Register(string userName, IClientHandler userHandler)
        {
            _registeredClients[userName] = userHandler;
        }

        public bool Unregister(string username)
        {
            IClientHandler handler;
            return _registeredClients.TryRemove(username, out handler);
        }
    }
}
