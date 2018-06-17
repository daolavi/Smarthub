using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class ChannelEmail
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public string Message { get; set; } // Message
        public string AccessToken { get; set; } // AccessToken
        public long? ExpiresInSeconds { get; set; } // ExpiresInSeconds
        public string IdToken { get; set; } // IdToken
        public System.DateTime? Issued { get; set; } // Issued
        public System.DateTime? IssuedUtc { get; set; } // IssuedUtc
        public string RefreshToken { get; set; } // RefreshToken
        public string Scope { get; set; } // Scope
        public string TokenType { get; set; } // TokenType
        public System.DateTime? LastSyncedDate { get; set; } // LastSyncedDate
    }
}
