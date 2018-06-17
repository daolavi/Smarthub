using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class Connection
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserChannelSource { get; set; } // UserChannelSource
        public int UserChannelTarget { get; set; } // UserChannelTarget
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? LastSyncedDate { get; set; } // LastSyncedDate
        public System.DateTime? NextSyncedDate { get; set; } // NextSyncedDate
        public Shared.Enums.ConnectionStatus Status { get; set; } // Status
        public int Counter { get; set; } // Counter
        public string Message { get; set; } // Message
        public bool IsActive { get; set; } // IsActive
    }
}
