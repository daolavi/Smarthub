using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class ChannelMagento
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public string Username { get; set; } // Username (length: 255)
        public string Password { get; set; } // Password (length: 255)
        public string Host { get; set; } // Host (length: 255)
        public string StoreId { get; set; } // StoreId (length: 50)
        public System.DateTime? LastSyncedDateTicket { get; set; } // LastSyncedDate_Ticket
        public System.DateTime? LastSyncedDateMessage { get; set; } // LastSyncedDate_Message
        public string Message { get; set; } // Message
        public string TimeZoneDisplayName { get; set; } // TimeZoneDisplayName (length: 255)
    }
}
