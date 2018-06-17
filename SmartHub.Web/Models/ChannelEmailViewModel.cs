using System;

namespace SmartHub.Web.Models
{
    public class ChannelEmailViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public long? ExpiresInSeconds { get; set; }

        public string IdToken { get; set; }

        public DateTime? Issued { get; set; }

        public DateTime? IssuedUtc { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public string TokenType { get; set; }

        public bool IsConnected { get; set; }
    }
}