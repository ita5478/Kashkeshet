using Kashkeshet.Common.KTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.BL.Abstractions
{
    public interface IRequestHandler
    {
        Task HandleRequest(KTPPacket requestPacket);
    }
}
