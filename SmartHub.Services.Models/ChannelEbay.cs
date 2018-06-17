using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class ChannelEbay
    {
        public int Id { get; set; } // Id (Primary key)
        public string Token { get; set; } // Token
        public System.DateTime? ExpiredDate { get; set; } // ExpiredDate
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public string Message { get; set; } // Message
        public System.DateTime? LastSyncedDateMemberMessage { get; set; } // LastSyncedDate_MemberMessage
    }
}
