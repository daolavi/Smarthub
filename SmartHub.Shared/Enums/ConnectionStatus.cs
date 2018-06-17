using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Shared.Enums
{
    public enum ConnectionStatus
    {
        Idle = 0,
        Connected = 1,
        Synchronizing = 2,
        Error=3,
    }
}
