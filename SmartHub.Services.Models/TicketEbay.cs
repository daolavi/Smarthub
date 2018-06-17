using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class TicketEbay
    {
        public int Id { get; set; } // Id (Primary key)
        public string EbayId { get; set; } // EbayId (length: 255)
        public int ConnectionId { get; set; } // ConnectionId
        public Shared.Enums.TicketStatus Status { get; set; } // Status
        public string Type { get; set; } // Type (length: 255)
        public string Subject { get; set; } // Subject (length: 1000)
        public string ItemId { get; set; } // ItemId (length: 255)
        public string CreatorId { get; set; } // CreatorId (length: 255)
        public string CreatorEmail { get; set; } // CreatorEmail (length: 255)
        public string RecipientId { get; set; } // RecipientId (length: 255)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? LastModifiedDate { get; set; } // LastModifiedDate
        public string Note { get; set; } // Note
        public System.DateTime? LastSynchronizedDate { get; set; } // LastSynchronizedDate
        public Shared.Enums.SyncStatus SyncStatus { get; set; } // SyncStatus
        public string SyncErrorMessage { get; set; } // SyncErrorMessage
    }
}
