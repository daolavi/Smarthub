using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class MessageMagento
    {
        public int Id { get; set; } // Id (Primary key)
        public string MagentoId { get; set; } // MagentoId (length: 255)
        public string SenderId { get; set; } // SenderId (length: 255)
        public string SenderEmail { get; set; } // SenderEmail (length: 255)
        public int TicketId { get; set; } // TicketId
        public string Message { get; set; } // Message
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? LastModifiedDate { get; set; } // LastModifiedDate
        public string Note { get; set; } // Note
        public System.DateTime? LastSynchronizedDate { get; set; } // LastSynchronizedDate
        public Shared.Enums.SyncStatus SyncStatus { get; set; } // SyncStatus
        public string SyncErrorMessage { get; set; } // SyncErrorMessage
    }
}
